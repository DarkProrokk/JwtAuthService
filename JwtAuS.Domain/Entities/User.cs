using System.ComponentModel.DataAnnotations;

namespace JwtAuS.Domain.Entities;

public class User
{
    [Key] 
    public Guid Id { get; set; }

    public required string Login { get; set; }

    public required string Email { get; set; }

    public bool EmailConfirmed { get; set; } = false;
    public required string PasswordHash { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; } = false;

    public bool TwoFactorEnabled { get; set; } = false;

    public int AccessFailedCount { get; set; }
    
    public required string Salt { get; set; }
    
}