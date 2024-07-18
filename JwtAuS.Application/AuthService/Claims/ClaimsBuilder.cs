using System.Security.Claims;

namespace JwtAuS.Application.AuthService.Claims;

public static class ClaimsBuilder
{
    public static List<Claim> GetClaims(Guid id, string login)
    {
        var loginClaim = new Claim(ClaimTypes.NameIdentifier, id.ToString());
        var emailClaim = new Claim(ClaimTypes.Name, login);
        
        var claims = new List<Claim>{loginClaim, emailClaim};
        
        return claims;
    }
}