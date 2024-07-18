using JwtAuS.Infrastructure.Data.Repository.Base.Interfaces;

namespace JwtAuS.Infrastructure.Data.Repository.User.Interfaces;

public interface IUserRepository: IBaseRepository<Domain.Entities.User>
{
    public Task<Domain.Entities.User?> GetByEmailAsync(string email);
}