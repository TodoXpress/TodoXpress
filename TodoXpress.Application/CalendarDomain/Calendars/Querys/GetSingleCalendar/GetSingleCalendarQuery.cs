using TodoXpress.Application.Contracts.MediatR;

namespace TodoXpress.Application;

public record class GetSingleCalendarQuery : IOneOfRequest<GetSingleCalendarResponse>
{
    public Guid CalendarId { get; set; }
}
