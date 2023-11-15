namespace TodoXpress.Application.CalendarDomain.Calendars.Commands.DeleteCalendar;

public class DeleteCalendarResponse
{
    public Guid CalendarId { get; set; }

    public bool Successful { get; set; }
}
