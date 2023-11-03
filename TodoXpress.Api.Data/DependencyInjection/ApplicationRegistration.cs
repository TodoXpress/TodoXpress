using Microsoft.EntityFrameworkCore;
using TodoXpress.Application;
using TodoXpress.Infastructure.Persistence.Contexts;

namespace TodoXpress.Api.Data.DependencyInjection;

/// <summary>
/// Contains methods to register dependencies from the application layer in the DI-Container.
/// </summary>
public static class ApplicationRegistration
{
    /// <summary>
    /// Adds MediatR to the DI-Container.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(x => 
            x.RegisterServicesFromAssembly(typeof(MediatRAnchor).Assembly));

        return services;
    }

    /// <summary>
    /// Adds the DbContext for the Calendar Domain.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="config">The <see cref="IConfiguration"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddCalendarDbContext(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<CalendarDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("Default")));

        return services;
    }
}
