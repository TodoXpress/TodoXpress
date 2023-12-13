using FluentValidation;
using FluentValidation.Results;
using OneOf;
using TodoXpress.Application.Contracts.MediatR;
using TodoXpress.Application.Contracts.Services.Calendars;
using TodoXpress.Domain;
using TodoXpress.Domain.Calendars;

namespace TodoXpress.Application.CalendarDomain.Calendars.Querys.GetSingleCalendar;

public class GetSingleCalendarQueryHandler : IOneOfRequestHandler<GetSingleCalendarQuery, GetSingleCalendarResponse>
{
    IValidator<GetSingleCalendarQuery> _validator;
    ICalendarService _calendarService;

    public GetSingleCalendarQueryHandler(IValidator<GetSingleCalendarQuery> validator, ICalendarService calendarData)
    {
        _validator = validator;
        _calendarService = calendarData;
    }

    public async Task<OneOf<GetSingleCalendarResponse, IError>> Handle(GetSingleCalendarQuery request, CancellationToken cancellationToken)
    {
        // validate request
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return new ValidationError<ValidationResult>()
            {
                ValidationErrorDescription = validationResult.ToString("|")
            };

        // fetch calendar
        var calendar = await _calendarService.ReadSingleAsync(request.CalendarId);
        if (calendar is null)
            return new ElementNotFoundError<Calendar>();

        return new GetSingleCalendarResponse() {
            Calendar = calendar
        };
    }
}
