namespace JwtAuS.Application.AuthService.Result.Interfaces;

public interface IBaseResult
{
    public bool Success { get; }
    
    public string? Error { get; }
}