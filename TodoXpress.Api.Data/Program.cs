using Carter;
using TodoXpress.Api.Data.DependencyInjection;
using TodoXpress.Infastructure;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add application layer
builder.Services
    .AddMediatR()
    .AddCarter();

// add infastructure layer
builder.Services
    .AddCalendarDbContext(config)
    .AddDataServices()
    .AddFluentValidation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapCarter();

app.Run();
