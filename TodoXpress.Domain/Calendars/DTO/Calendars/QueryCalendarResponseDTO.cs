using TodoXpress.Domain.Common;

namespace TodoXpress.Domain.Calendars.DTO.Calendars;

/// <summary>
/// the datatransfer object for querying a calendar
/// </summary>
public record QueryCalendarResponseDTO
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public Color? Color { get; set; }

    public List<Guid> EventIds { get; set; } = [];
}
