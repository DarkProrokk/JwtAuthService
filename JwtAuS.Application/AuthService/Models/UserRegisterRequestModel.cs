namespace JwtAuS.Application.AuthService.Models;

public class UserRegisterRequestModel(Guid guid, string? email, string? login)
{
    public Guid Guid { get; set; } = guid;

    public string? Email { get; set; } = email;

    public string? Login { get; set; } = login;
}