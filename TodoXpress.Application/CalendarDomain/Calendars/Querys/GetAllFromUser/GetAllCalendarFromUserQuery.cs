using TodoXpress.Application.Contracts.MediatR;

namespace TodoXpress.Application.CalendarDomain.Calendars.Querys.GetAllFromUser;

public record class GetAllCalendarFromUserQuery : IOneOfRequest<GetAllCalendarFromUserResponse>
{
    public required Guid UserId { get; set; }
}
