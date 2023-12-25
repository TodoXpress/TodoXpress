using Carter;
using TodoXpress.Api.Identity;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// swagger
builder.Services.AddSwagger();

// identity
builder.Services.AddAspIdentity(config);

// auth
builder.Services.AddAuthentication(config);
builder.Services.AddAuthentication();

// services
builder.Services.AddServices();
builder.Services.AddCarter();

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
