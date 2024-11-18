using JwtAuS.Application.AuthService;
using JwtAuS.Application.AuthService.Interfaces;
using JwtAuS.Application.QueueService;
using JwtAuS.Application.QueueService.Interfaces;
using JwtAuS.Infrastructure.Data.Context;
using JwtAuS.Infrastructure.Data.Repository.User;
using JwtAuS.Infrastructure.Data.Repository.User.Interfaces;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
var rabbitSettings = builder.Configuration.GetSection("RabbitMQ");
builder.Services.AddSingleton<ConnectionFactory>(sp =>
{
    return new ConnectionFactory()
    {
        HostName = rabbitSettings["HostName"],
        UserName = rabbitSettings["UserName"],
        Password = rabbitSettings["Password"],
        VirtualHost = "/",
        Port = AmqpTcpEndpoint.UseDefaultPort
    };
});
builder.Services.AddSingleton<IQueueService, QueueService>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(corsPolicyBuilder => corsPolicyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AuthContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
