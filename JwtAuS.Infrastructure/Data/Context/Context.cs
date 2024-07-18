using JwtAuS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JwtAuS.Infrastructure.Data.Context;

public class AuthContext: DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql("server=127.0.0.1;port=3306;database=JwtAuthenticationService;uid=root;pwd=123", ServerVersion.Parse("8.0.32-mysql"));
    }

    public DbSet<User> Users { get; set; }
    
}