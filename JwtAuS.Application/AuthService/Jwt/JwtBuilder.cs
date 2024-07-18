using System.IdentityModel.Tokens.Jwt;
using JwtAuS.Application.AuthService.Jwt.Options;
using Microsoft.IdentityModel.Tokens;
using static JwtAuS.Application.AuthService.Claims.ClaimsBuilder;
namespace JwtAuS.Application.AuthService.Jwt;

public static class JwtBuilder
{
    public static string GetJwt(Guid id, string email)
    {
        var claims = GetClaims(id, email);
        
        var jwt = new JwtSecurityToken(
                issuer: MoneyTrackingAuthOptions.Issuer,
                audience: MoneyTrackingAuthOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                signingCredentials:new SigningCredentials(MoneyTrackingAuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        return encodedJwt;
    }
}