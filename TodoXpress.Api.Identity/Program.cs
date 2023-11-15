using Microsoft.EntityFrameworkCore;
using TodoXpress.Api.Identity.Entities;
using TodoXpress.Api.Identity.Persistence;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseNpgsql(config.GetConnectionString("Default")));

builder.Services
    .AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<IdentityContext>();

// Configure the HTTP request pipeline.
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<User>();

app.UseHttpsRedirection();

app.Run();
