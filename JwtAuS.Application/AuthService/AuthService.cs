using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using JwtAuS.Application.AuthService.Interfaces;
using JwtAuS.Application.AuthService.Jwt;
using JwtAuS.Application.AuthService.Models;
using JwtAuS.Application.AuthService.Result;
using JwtAuS.Application.AuthService.Result.Interfaces;
using JwtAuS.Domain.Entities;
using JwtAuS.Infrastructure.Data.Repository.User.Interfaces;
using RabbitMQ.Client;
using static JwtAuS.Application.AuthService.Tools.PasswordHashGenerator;
namespace JwtAuS.Application.AuthService;

public class AuthService(IUserRepository userRepository) : IAuthService
{
    public async Task<IBaseResult> RegisterAsync(UserRegisterModel model)
    {
        var result = await RegisterLocallyAsync(model);
        if (result.Success is false) return result;
        PublishUserRegisteredEvent(result.Model);
        return (BaseResult)result;
    }

    private async Task<IRegisteredResult> RegisterLocallyAsync(UserRegisterModel user)
    {
        var result = new RegisteredResult();
        
        var entity = await userRepository.GetByEmailAsync(user.Email);
        if (entity is not null)
        {
            result.Error = "User already exists";
            return result;
        }
        
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
        result.Model = registerRequestModel;
        result.Success = true;
        
        return result;
    }

    public async Task<IAuthResult> LoginAsync(UserLoginModel model)
    {
        var user = await userRepository.GetByEmailAsync(model.Email);
        if (user is null) return new AuthResult(error: "User not found");


        if (!ValidatePassword(model.Password, user.Salt, user.PasswordHash))
            return new AuthResult(error: "Wrong password");

        var token = JwtBuilder.GetJwt(user.Id, user.Login);
        return new AuthResult(true, token: token);
    }

    #region Tools

    private bool ValidatePassword(string password, string salt, string storedHash)
    {
        return GenerateHashPassword(password, salt) == storedHash;
    }
    

    private static void PublishUserRegisteredEvent(UserRegisterRequestModel registeredEvent)
    {
        var factory = new ConnectionFactory();
        factory.HostName = "91.184.241.3";
        factory.UserName = "DarkProrok";
        factory.Password = "Sameva72";
        factory.VirtualHost = "/";
        factory.Port = AmqpTcpEndpoint.UseDefaultPort;

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "user_registered", durable: false, exclusive: false, autoDelete: false,
            arguments: null);

        var messageBody = JsonSerializer.Serialize(registeredEvent);
        var body = Encoding.UTF8.GetBytes(messageBody);

        channel.BasicPublish(exchange: "", routingKey: "user_registered", basicProperties: null, body: body);
    }

    #endregion
}