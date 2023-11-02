using System.Drawing;

namespace TodoXpress.Application.CalendarDomain.Calendars.Commands.CreateCalendar;

public record class CreateCalendarCommand : IOneOfRequest<Guid>
{
    public required string Name { get; set; }

    public KnownColor Color { get; set; }
}
