using TodoXpress.Application.Contracts.MediatR;
using TodoXpress.Domain.Common.ValueTypes;

namespace TodoXpress.Application.CalendarDomain.Calendars.Commands.UpdateCalendar;

public class UpdateCalendarCommand : IOneOfRequest<UpdateCalendarResponse>
{
    public Guid CalendarId { get; set; }

    public string? NewCalendarName { get; set; }

    public ARGBColor? NewColor { get; set; }
}
