using JwtAuS.Application.AuthService.Interfaces;
using JwtAuS.Application.AuthService.Models.Interfaces;
using JwtAuS.Application.AuthService.Result;
using JwtAuS.Application.AuthService.Result.Interfaces;

namespace JwtAuS.Application.AuthService.Models;

public class RegisteredResult(UserRegisterRequestModel? model = null, bool success = false, string? error = null): BaseResult(success, error), IRegisteredResult
{
    public UserRegisterRequestModel? Model { get; set; } = model;
}