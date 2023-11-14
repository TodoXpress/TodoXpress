using TodoXpress.Application.Contracts.MediatR;

namespace TodoXpress.Application.CalendarDomain.Calendars.Commands.DeleteCalendar;

public class DeleteCalendarCommand : IOneOfRequest<DeleteCalendarResponse>
{
    public Guid CalendarId { get; set; }
}
