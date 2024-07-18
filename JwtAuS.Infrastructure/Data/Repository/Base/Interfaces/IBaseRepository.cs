namespace JwtAuS.Infrastructure.Data.Repository.Base.Interfaces;

public interface IBaseRepository<T> where T: class
{
    public Task AddAsync(T entity);
    
    public Task SaveChangesAsync();
}