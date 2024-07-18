using JwtAuS.Application.AuthService;
using JwtAuS.Application.AuthService.Interfaces;
using JwtAuS.Infrastructure.Data.Context;
using JwtAuS.Infrastructure.Data.Repository.Base.Interfaces;
using JwtAuS.Infrastructure.Data.Repository.User;
using JwtAuS.Infrastructure.Data.Repository.User.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserRepository, UserRepository >();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddControllers();


builder.Services.AddDbContext<AuthContext>();
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

app.UseRouting();
app.MapControllers();

app.Run();