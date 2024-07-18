using JwtAuS.Application.AuthService.Interfaces;

namespace JwtAuS.Application.AuthService.Result.Interfaces;

public interface IAuthResult: IBaseResult
{

    public string? Token { get; }
    

}