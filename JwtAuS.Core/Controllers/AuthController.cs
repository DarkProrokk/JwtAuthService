using JwtAuS.Application.AuthService.Interfaces;
using JwtAuS.Application.AuthService.Models;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuS.Core.Controllers;

[Route("auth/")]
public class AuthController: Controller
{
    
    [Route("register/")]
    [HttpGet]
    public IActionResult RegisterUser()
    {
        return View();
    }
    
    [Route("register/")]
    [HttpPost]
    public IActionResult RegisterUser(UserRegisterModel model)
    {
        if (!model.IsValid()) return View();
        
        return View();
    }
    
    [Route("login/")]
    [HttpGet]
    public IActionResult LoginUser()
    {
        return View();
    }
}