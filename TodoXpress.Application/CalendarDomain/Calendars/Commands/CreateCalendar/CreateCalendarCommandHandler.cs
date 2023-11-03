using MediatR;
using OneOf;
using TodoXpress.Application.Contracts.MediatR;
using TodoXpress.Domain;

namespace TodoXpress.Application.CalendarDomain.Calendars.Commands.CreateCalendar;

public class CreateCalendarCommandHandler : IOneOfRequestHandler<CreateCalendarCommand, Guid>
{
    public Task<OneOf<Guid, IError>> Handle(CreateCalendarCommand request, CancellationToken cancellationToken)
    {
        // validate request

        // check if calendar exists

        // save calendar

        // return id of the calendar

        return default;
    }

}
