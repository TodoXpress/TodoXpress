using TodoXpress.Application.Contracts.Persistence;
using TodoXpress.Application.Contracts.Persistence.Common;
using TodoXpress.Domain.Calendars;
using TodoXpress.Domain.Common;

namespace TodoXpress.Application.Contracts.Services.Calendars;

public interface ICalendarService : 
    IReadableDataService<Calendar>,
    ICreateableDataService<Calendar>,
    IUpdateableDataService<Calendar>,
    IDeleteableDataService<Calendar>, 
    IUnitOfWork
{
    /// <summary>
    /// Reads all calendar from a user from the persistence.
    /// </summary>
    /// <param name="user">The user from wich the calendars should be queried.</param>
    /// <returns>A list with the calendars.</returns>
    Task<List<Calendar>> ReadAllFromUserAsync(Domain.Calendars.User user);

    void UpdateValues(Calendar calendar, string? newName, Color? newColor);
}
