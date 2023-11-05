using Microsoft.EntityFrameworkCore;
using FluentValidation;
using TodoXpress.Infastructure;
using TodoXpress.Infastructure.Persistence.Contexts;

namespace TodoXpress.Api.Data.DependencyInjection;

/// <summary>
/// Contains methods to register dependencies from the infastructure layer in the DI-Container.
/// </summary>
public static class InfastructureRegistration
{
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

    /// <summary>
    /// Adds the validators from Fluentvalidation.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateCalendarValidator>(ServiceLifetime.Scoped);

        return services;
    }
}
