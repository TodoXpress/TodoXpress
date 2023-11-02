using System.Drawing;

namespace TodoXpress.Application.CalendarDomain.Calendars.Querys.GetSingleCalendar;

public record class GetSingleCalendarResponse
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public Color Color { get; set; }

    public List<Guid> EventIds { get; set; } = [];
}
