using TodoXpress.Application.Contracts.Persistence.Common;
using TodoXpress.Domain.Calendars;

namespace TodoXpress.Application.Contracts.Persistence.Services;

/// <summary>
/// Defines a service to interact with a database for the calendars.
/// </summary>
public interface ICalendarDataService: 
    IReadableDataService<Calendar>, 
    ICreateableDataService<Calendar>,
    IUpdateableDataService<Calendar>,
    IDeleteableDataService<Calendar>
{
    /// <summary>
    /// Reads all calendar from a user from the persistence.
    /// </summary>
    /// <param name="user">The user from wich the calendars should be queried.</param>
    /// <returns>A list with the calendars.</returns>
    Task<List<Calendar>> ReadAllFromUserAsync(User user);
}
