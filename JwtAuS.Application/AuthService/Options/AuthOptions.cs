using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuS.Application.Options;

public class AuthOptions
{
    public const string Issuer = "JwtAuS"; // издатель токена
    public const string Audience = "MyAuthClient"; // потребитель токена
    const string Key = "R39Q8m%%*svIWN%zBRkcIXa@%3gss#iY";   // ключ для шифрации
    public const int Lifetime = 1; // время жизни токена - 1 минута
    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
    }
}