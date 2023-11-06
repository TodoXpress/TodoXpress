using Microsoft.Extensions.DependencyInjection;
using TodoXpress.Application.Contracts.Persistence.Services;
using TodoXpress.Application.Contracts.Persistence;
using TodoXpress.Infastructure.Persistence.Services.Calendars;

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
}
