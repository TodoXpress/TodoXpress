using TodoXpress.Domain.Calendars;

namespace TodoXpress.Application;

public record class GetSingleCalendarResponse
{
    public required Calendar Calendar { get; set; }
}
