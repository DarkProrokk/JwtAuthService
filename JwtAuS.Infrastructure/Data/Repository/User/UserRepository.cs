using JwtAuS.Infrastructure.Data.Context;
using JwtAuS.Infrastructure.Data.Repository.Base;
using JwtAuS.Infrastructure.Data.Repository.User.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JwtAuS.Infrastructure.Data.Repository.User;

public class UserRepository(AuthContext context) : BaseRepository<Domain.Entities.User>(context), IUserRepository
{
    public Task<Domain.Entities.User?> GetByEmailAsync(string email)
    {
        return context.Users
            .FirstOrDefaultAsync(x => x.Email == email);
    }
}