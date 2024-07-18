using JwtAuS.Application.AuthService.Result.Interfaces;

namespace JwtAuS.Application.AuthService.Result;

public class AuthResult(bool success = false, string? error = null, string? token = null): BaseResult(success, error), IAuthResult
{
    public string? Token { get; set; } = token;
}