using TodoXpress.Domain.Common.ValueTypes;

namespace TodoXpress.Domain.Calendars.DTO;

/// <summary>
/// Represents the datatransfer object for creating a calendar.
/// </summary>
public record struct CreateCalendarDTO
{
    public string Name { get; set; }

    public ARGBColor Color { get; set; }
}
