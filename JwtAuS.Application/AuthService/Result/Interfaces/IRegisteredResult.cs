namespace JwtAuS.Application.AuthService.Result.Interfaces;

public interface IRegisteredResult: IBaseResult
{
    public Guid Guid { get; set; }
    
    public string? Email { get; set; }
    
    public string? Login { get; set; }
}