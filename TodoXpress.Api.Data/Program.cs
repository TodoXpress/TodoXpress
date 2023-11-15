using Carter;
using TodoXpress.Api.Data.DependencyInjection;
using TodoXpress.Infastructure;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

// add application layer
builder.Services
    .AddMediatR()
    .AddCarter();

// add infastructure layer
builder.Services
    .AddCalendarDbContext(config)
    .AddDataServices()
    .AddFluentValidation();

WebApplication app = builder.Build();

app.UseSwaggerInDev();

app.UseHttpsRedirection();

app.MapCarter();

app.Run();
