using JwtAuS.Application.AuthService.Interfaces;
using JwtAuS.Application.AuthService.Models.Interfaces;
using JwtAuS.Application.AuthService.Result;
using JwtAuS.Application.AuthService.Result.Interfaces;

namespace JwtAuS.Application.AuthService.Models;

public class RegisteredResult(UserRegisterRequestModel model, bool success = false, string? error = null): BaseResult(success, error), IRegisteredResult
{
    public Guid Guid { get; set; } = model.Guid;

    public string? Email { get; set; } = model.Email;

    public string? Login { get; set; } = model.Login;
}