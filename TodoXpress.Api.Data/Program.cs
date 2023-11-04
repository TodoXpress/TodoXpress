using Carter;
using TodoXpress.Api.Data.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add application layer
builder.Services.AddMediatR();
builder.Services.AddCarter();

// add infastructure layer
builder.Services.AddCalendarDbContext(config);
builder.Services.AddFluentValidation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapCarter();

app.UseHttpsRedirection();

app.Run();
