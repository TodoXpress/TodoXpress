using TodoXpress.Application.Contracts.Persistence.Common;
using TodoXpress.Domain.Calendars;

namespace TodoXpress.Application.Contracts.Persistence;

public interface ICalendarDataService: 
    IReadableDataService<Calendar>, 
    ICreateableDataService<Calendar>,
    IUpdateableDataService<Calendar>,
    IDeleteableDataService<Calendar>
{
    Task<List<Calendar>> ReadAllFromUserAsync(User user);
}
