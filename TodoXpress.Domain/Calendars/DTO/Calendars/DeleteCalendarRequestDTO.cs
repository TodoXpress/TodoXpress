namespace TodoXpress.Domain.Calendars.DTO.Calendars;

public record struct DeleteCalendarRequestDTO
{
    public Guid CalendarId { get; set; }
}
