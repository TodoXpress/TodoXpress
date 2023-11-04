using System.Drawing;
using TodoXpress.Application.Contracts.MediatR;
using TodoXpress.Domain.Calendars;

namespace TodoXpress.Application.CalendarDomain.Calendars.Commands.CreateCalendar;

public record class CreateCalendarCommand : IOneOfRequest<Guid>
{
    public required string Name { get; set; }

    public required User User { get; set; }

    public Color Color { get; set; }
}
