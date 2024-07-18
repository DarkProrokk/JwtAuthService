using JwtAuS.Infrastructure.Data.Context;
using JwtAuS.Infrastructure.Data.Repository.Base.Interfaces;

namespace JwtAuS.Infrastructure.Data.Repository.Base;

public class BaseRepository<T>(AuthContext context): IBaseRepository<T> where T: class
{
    public async Task AddAsync(T entity)
    { 
        await context.Set<T>().AddAsync(entity);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}