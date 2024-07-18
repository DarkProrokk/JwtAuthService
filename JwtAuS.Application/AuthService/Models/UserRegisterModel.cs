namespace JwtAuS.Application.AuthService.Models;

public class UserRegisterModel
{
    public required string Login { get; set; }

    public required string Email { get; set; }
    
    public required string Password { get; set; }

    public string? PhoneNumber { get; set; }
    
}

public static class UserRegisterModelExtensions
{
    public static bool IsValid(this UserRegisterModel model)
    {
        return !string.IsNullOrEmpty(model.Login) && !string.IsNullOrEmpty(model.Email) && !string.IsNullOrEmpty(model.Password);
    }
}