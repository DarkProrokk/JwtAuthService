using JwtAuS.Application.AuthService.Models;

namespace JwtAuS.Application.AuthService.Result.Interfaces;

public interface IRegisteredResult: IBaseResult
{
    public UserRegisterRequestModel? Model { get; }
}