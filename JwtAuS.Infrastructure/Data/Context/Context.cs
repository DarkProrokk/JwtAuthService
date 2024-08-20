using JwtAuS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JwtAuS.Infrastructure.Data.Context;

public class AuthContext: DbContext
{

    public AuthContext(DbContextOptions<AuthContext> options) : base(options)
    {
        
    }

    public DbSet<User> Users { get; set; }
    
}