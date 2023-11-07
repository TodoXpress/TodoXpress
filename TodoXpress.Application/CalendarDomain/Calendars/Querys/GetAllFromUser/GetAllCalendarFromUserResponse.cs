using TodoXpress.Domain.Calendars;

namespace TodoXpress.Application.CalendarDomain.Calendars.Querys.GetAllFromUser;

public record class GetAllCalendarFromUserResponse
{
    public List<Calendar> Calendars { get; set; } = [];

    public Guid UserId { get; set; }
}
