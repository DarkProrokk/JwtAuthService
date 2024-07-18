using System.Text;
using System.Text.Json;
using JwtAuS.Application.AuthService.Interfaces;
using JwtAuS.Application.AuthService.Models;
using JwtAuS.Application.AuthService.Result.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace JwtAuS.Core.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterUserAsync(UserRegisterModel model)
    {
        if (!model.IsValid()) return BadRequest();
        var result = await RegisterUserLocallyAsync(model);
        if (!result.Success) return Unauthorized(result.Error);
        PublishUserRegisteredEvent(result);

        return Ok("User registered successfully");
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> LoginUserAsync(UserLoginModel model)
    {
        if (!model.IsValid()) return BadRequest();
        var result = await authService.LoginAsync(model);
        if (result.Success) return Ok(result.Token);
        return Unauthorized(result.Error);
    }

    private async Task<IRegisteredResult> RegisterUserLocallyAsync(UserRegisterModel model)
    {
        var result = await authService.RegisterAsync(model);
        return result;
    }

    private static void PublishUserRegisteredEvent(IRegisteredResult registeredEvent)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "user_registered", durable: false, exclusive: false, autoDelete: false,
            arguments: null);

        var messageBody = JsonSerializer.Serialize(registeredEvent);
        var body = Encoding.UTF8.GetBytes(messageBody);

        channel.BasicPublish(exchange: "", routingKey: "user_registered", basicProperties: null, body: body);
    }
}