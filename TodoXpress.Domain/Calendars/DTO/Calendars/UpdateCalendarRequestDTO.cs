using TodoXpress.Domain.Common.ValueTypes;

namespace TodoXpress.Domain.Calendars.DTO.Calendars;

/// <summary>
/// Represents the datatransfer object for updating a calendar.
/// </summary>
public record struct UpdateCalendarRequestDTO
{
    public Guid CalendarId { get; set; }

    public string Name { get; set; }

    public ARGBColor Color { get; set; }
}
