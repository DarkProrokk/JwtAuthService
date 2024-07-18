using JwtAuS.Application.AuthService.Result.Interfaces;

namespace JwtAuS.Application.AuthService.Result;

public class BaseResult(bool success = false, string? error = null): IBaseResult
{
    public bool Success { get; set; } = success;
    public string? Error { get; set; } = error;
}