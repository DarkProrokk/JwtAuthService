using JwtAuS.Application.AuthService.Models;
using JwtAuS.Application.AuthService.Result.Interfaces;

namespace JwtAuS.Application.AuthService.Interfaces;

public interface IAuthService
{
    public Task<IBaseResult> RegisterAsync(UserRegisterModel model);
    public Task<IAuthResult> LoginAsync(UserLoginModel model);
    //public Task<AuthResult> RefreshTokenAsync(string token, string refreshToken);
}