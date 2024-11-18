using JwtAuS.Application.AuthService.Interfaces;
using JwtAuS.Application.AuthService.Models;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuS.Core.Controllers;

[ApiController]
[Route("api/auth/")]
public class AuthApiController(IAuthService authService) : ControllerBase
{
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterUserAsync(UserRegisterModel model)
    {
        if (!model.IsValid()) return BadRequest();
        var result = await authService.RegisterAsync(model);
        return Ok(result);
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> LoginUserAsync(UserLoginModel model)
    {
        if (!model.IsValid()) return BadRequest();
        var result = await authService.LoginAsync(model);
        Response.Cookies.Append("accessToken", result.Token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true, // Это будет работать только через HTTPS
            Domain = ".dark.dev",
            Path = "/",
            SameSite = SameSiteMode.None,
            Expires = DateTimeOffset.UtcNow.AddHours(1)
        });
        if (result.Success) return Ok(result);
        return Unauthorized(result.Error);
    }


}