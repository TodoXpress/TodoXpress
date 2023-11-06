using TodoXpress.Application.Contracts.MediatR;
using TodoXpress.Domain.Calendars;

namespace TodoXpress.Application;

public record class GetSingleCalendarQuery : IOneOfRequest<Calendar>
{
    public Guid CalendarId { get; set; }
}
