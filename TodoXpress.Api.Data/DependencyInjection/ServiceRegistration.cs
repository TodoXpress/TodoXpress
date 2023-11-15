using System.Reflection;
using Microsoft.OpenApi.Models;

namespace TodoXpress.Api.Data.DependencyInjection;

/// <summary>
/// Provides static methods to register and configure services.
/// </summary>
public static class ServiceRegistration
{
    /// <summary>
    /// Adds a configured Swagger gen to the services.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(config => 
        {
            config.SwaggerDoc("v1", new OpenApiInfo()
            {
                Version = "v1",
                Title = "TodoXpress Data API",
                Description = "API of the TodoXpress App for manageing the data",
                Contact = new OpenApiContact()
                {
                    Name = "Fabian Daser",
                    Email = "fabian@todoxpress.com",
                }
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            config.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        return services;
    }
}
