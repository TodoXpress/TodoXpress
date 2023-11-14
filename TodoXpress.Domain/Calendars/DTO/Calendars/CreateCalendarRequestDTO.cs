using TodoXpress.Domain.Common.ValueTypes;

namespace TodoXpress.Domain.Calendars.DTO.Calendars;

/// <summary>
/// Represents the datatransfer object for creating a calendar.
/// </summary>
public record struct CreateCalendarRequestDTO
{
    public string Name { get; set; }

    public ARGBColor Color { get; set; }
}
