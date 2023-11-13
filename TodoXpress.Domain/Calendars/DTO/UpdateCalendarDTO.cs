using TodoXpress.Domain.Common.ValueTypes;

namespace TodoXpress.Domain;

/// <summary>
/// Represents the datatransfer object for updating a calendar.
/// </summary>
public record struct UpdateCalendarDTO
{
    public Guid CalendarId { get; set; }

    public string Name { get; set; }

    public ARGBColor Color { get; set; }
}
