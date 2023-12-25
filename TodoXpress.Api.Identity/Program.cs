using Carter;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoXpress.Api.Identity.Services;
using TodoXpress.Api.Identity.Entities;
using TodoXpress.Api.Identity.Persistence;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCarter();

// identity
builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseNpgsql(config.GetConnectionString("Default")));

builder.Services
    .AddIdentity<User, Role>()
    .AddRoleStore<RoleStore<Role, IdentityContext, Guid>>()
    .AddRoleManager<RoleManager<Role>>()
    .AddUserStore<UserStore<User, Role, IdentityContext, Guid>>()
    .AddUserManager<UserManager<User>>()
    .AddSignInManager();

// auth
builder.Services.AddAuthentication(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();

// services
builder.Services.AddScoped<IdentityService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

var version = app.MapGroup("v1");

version.MapCarter();
version.MapGet("",() => Results.Ok());

app.Run();
