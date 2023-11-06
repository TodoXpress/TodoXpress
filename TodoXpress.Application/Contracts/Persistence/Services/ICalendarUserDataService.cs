using TodoXpress.Application.Contracts.Persistence.Common;
using TodoXpress.Domain.Calendars;

namespace TodoXpress.Application.Contracts.Persistence.Services;

/// <summary>
/// Defines a service to interact with a database for the user of the calendar.
/// </summary>
public interface ICalendarUserDataService
    : IReadableDataService<User>, 
    ICreateableDataService<User>,
    IUpdateableDataService<User>,
    IDeleteableDataService<User>
{
}
