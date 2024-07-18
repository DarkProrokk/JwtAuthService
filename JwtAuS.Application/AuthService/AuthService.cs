using System.Buffers.Text;
using System.Security.Cryptography;
using JwtAuS.Application.AuthService.Interfaces;
using JwtAuS.Application.AuthService.Jwt;
using JwtAuS.Application.AuthService.Models;
using JwtAuS.Application.AuthService.Models.Interfaces;
using JwtAuS.Application.AuthService.Result;
using JwtAuS.Application.AuthService.Result.Interfaces;
using JwtAuS.Domain.Entities;
using JwtAuS.Infrastructure.Data.Repository.User.Interfaces;

namespace JwtAuS.Application.AuthService;

public class AuthService(IUserRepository userRepository): IAuthService
{
    public async Task<IRegisteredResult> RegisterAsync(UserRegisterModel user)
    {
        var salt = GenerateSalt();
        var hashPassword = GenerateHashPassword(user.Password, salt);
        var userEntity = new User
        {
            Email = user.Email,
            Login = user.Login,
            PasswordHash = hashPassword,
            Salt = salt
        };
        await userRepository.AddAsync(userEntity);
        await userRepository.SaveChangesAsync();

        var registerRequestModel = new UserRegisterRequestModel(userEntity.Id, userEntity.Email, userEntity.Login);
        var registeredResult = new RegisteredResult(registerRequestModel, true);
        return registeredResult;
    }

    public async Task<IAuthResult> LoginAsync(UserLoginModel model)
    {
        var user = await userRepository.GetByEmailAsync(model.Email);
        if (user is null) return new AuthResult(error: "User not found");


        if (!ValidatePassword(model.Password, user.Salt, user.PasswordHash))
            return new AuthResult(error:"Wrong password");
        
        var token = JwtBuilder.GetJwt(user.Id, user.Login);
        return new AuthResult(true, token:token);
    }

    private bool ValidatePassword(string password, string salt, string storedHash)
    {
        return GenerateHashPassword(password, salt) == storedHash;
    }
    
    private string GenerateHashPassword(string password, string salt)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512(Convert.FromBase64String(salt)))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(computedHash);
        }
    }
    
    private string GenerateSalt(int size = 16)
    {
        byte[] saltBytes = new byte[size];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(saltBytes);
        }

        return Convert.ToBase64String(saltBytes);
    }
}