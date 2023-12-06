using System.Security.Claims;
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

// identity
builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseNpgsql(config.GetConnectionString("Default")));

builder.Services
    .AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<IdentityContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication()
    .UseAuthorization();

app.MapIdentityApi<User>();

app.Run();
