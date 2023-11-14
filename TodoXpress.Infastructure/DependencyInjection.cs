using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoXpress.Application.Contracts.Persistence.Services;
using TodoXpress.Application.Contracts.Persistence;
using TodoXpress.Infastructure.Persistence.Services.Calendars;
using TodoXpress.Infastructure.Persistence.Contexts;
using TodoXpress.Infastructure.Validation.Calendar;
using FluentValidation;

namespace TodoXpress.Infastructure;

public static class DependencyInjection
{
    /// <summary>
    /// Adds all services for interacting with the dbcontexts.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddDataServices(this IServiceCollection services)
    {
        services.AddScoped<ICalendarUnitOfWork, CalendarUnitOfWork>();
        services.AddScoped<ICalendarDataService, CalendarService>();
        services.AddScoped<ICalendarUserDataService, CalendarUserService>();

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

    /// <summary>
    /// Adds the validators from Fluentvalidation.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateCalendarValidator>(ServiceLifetime.Singleton);

        return services;
    }
}
