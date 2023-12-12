using TodoXpress.Application.Contracts.Persistence.Common;
using TodoXpress.Application.Contracts.Persistence;
using TodoXpress.Domain.Calendars;

namespace TodoXpress.Application.Contracts.Services.Calendars;

public interface ICalendarUserService :
    IReadableDataService<User>,
    ICreateableDataService<User>,
    IUpdateableDataService<User>,
    IDeleteableDataService<User>,
    IUnitOfWork
{
}
