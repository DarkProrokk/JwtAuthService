using System.Security.Cryptography;
using System.Text;

namespace JwtAuS.Application.AuthService.Tools;

public static class PasswordHashGenerator
{
    public static string GenerateHashPassword(string password, string salt)
    {
        using (var hmac = new HMACSHA512(Convert.FromBase64String(salt)))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(computedHash);
        }
    }

    public static string GenerateSalt(int size = 16)
    {
        byte[] saltBytes = new byte[size];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(saltBytes);
        }

        return Convert.ToBase64String(saltBytes);
    }
}