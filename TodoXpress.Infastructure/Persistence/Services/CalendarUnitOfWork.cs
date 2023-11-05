using TodoXpress.Application.Contracts.Persistence;
using TodoXpress.Infastructure.Persistence.Contexts;
using TodoXpress.Infastructure.Persistence.Services.Base;

namespace TodoXpress.Infastructure;

public class CalendarUnitOfWork(CalendarDbContext context) : UnitOfWork<CalendarDbContext>(context), ICalendarUnitOfWork
{
}
