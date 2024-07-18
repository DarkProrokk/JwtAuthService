namespace JwtAuS.Application.AuthService.Models;

public class UserLoginModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}


public static class UserLoginModelExtensions
{
    public static bool IsValid(this UserLoginModel model)
    {
        return !string.IsNullOrEmpty(model.Email) && !string.IsNullOrEmpty(model.Password);
    }
}