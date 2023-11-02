using OneOf;
using TodoXpress.Application.Contracts.MediatR;
using TodoXpress.Domain;

namespace TodoXpress.Application.CalendarDomain.Calendars.Querys.GetSingleCalendar;

public class GetSingleCalendarQueryHandler : IOneOfRequestHandler<GetSingleCalendarQuery, GetSingleCalendarResponse>
{
    public Task<OneOf<GetSingleCalendarResponse, IError>> Handle(GetSingleCalendarQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
