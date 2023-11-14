namespace TodoXpress.Domain.Calendars.DTO.Calendars;

public record struct UpdateCalendarResponseDTO
{
    public Guid CalendarId { get; set; }

    public bool Success { get; set; }
}
