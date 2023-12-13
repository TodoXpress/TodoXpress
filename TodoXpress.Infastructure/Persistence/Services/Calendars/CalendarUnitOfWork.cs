using TodoXpress.Infastructure.Persistence.Contexts;
using TodoXpress.Infastructure.Persistence.Services.Base;

namespace TodoXpress.Infastructure.Persistence.Services.Calendars;

internal class CalendarUnitOfWork(CalendarDbContext context) : UnitOfWork<CalendarDbContext>(context)
{
}
