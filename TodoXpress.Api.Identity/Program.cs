using Carter;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoXpress.Api.Identity;
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
    // .AddIdentityApiEndpoints<User>()
    // .AddEntityFrameworkStores<IdentityContext>();
    .AddIdentity<User, Role>()
    .AddRoleStore<RoleStore<Role, IdentityContext, Guid>>()
    .AddRoleManager<RoleManager<Role>>()
    .AddUserStore<UserStore<User, Role, IdentityContext, Guid>>()
    .AddUserManager<UserManager<User>>()
    .AddSignInManager();

builder.Services.AddAuthentication(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

//app.MapIdentityApi<User>();
app.MapCarter();

app.Run();
