using FluentValidation;
using FluentValidation.Results;
using OneOf;
using TodoXpress.Application.Contracts.MediatR;
using TodoXpress.Application.Contracts.Persistence.Common;
using TodoXpress.Application.Contracts.Persistence.Services;
using TodoXpress.Application.Contracts.Services.Calendars;
using TodoXpress.Domain;
using TodoXpress.Domain.Calendars;

namespace TodoXpress.Application.CalendarDomain.Calendars.Querys.GetAllFromUser;

public class GetAllFromUserQueryHandler : IOneOfRequestHandler<GetAllCalendarFromUserQuery, GetAllCalendarFromUserResponse>
{
    IValidator<GetAllCalendarFromUserQuery> _validator;
    ICalendarService _calendarService;
    IReadableDataService<User> _userService;

    public GetAllFromUserQueryHandler(ICalendarUserService userService, ICalendarService calendarService, IValidator<GetAllCalendarFromUserQuery> validator)
    {
        _userService = userService;
        _calendarService = calendarService;
        _validator = validator;
    }

    public async Task<OneOf<GetAllCalendarFromUserResponse, IError>> Handle(GetAllCalendarFromUserQuery request, CancellationToken cancellationToken)
    {
        // validate Request
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return new ValidationError<ValidationResult>()
            {
                ValidationErrorDescription = validationResult.ToString("|"),
            };

        var user = await _userService.ReadSingleAsync(request.UserId);
        if (user is null)
            return new ElementNotFoundError<User>();

        var calendars = await _calendarService.ReadAllFromUserAsync(user);
        if (calendars is [ ])
            return new ElementNotFoundError<List<Calendar>>();

        return new GetAllCalendarFromUserResponse()
        {
            Calendars = calendars,
            UserId = user.Id
        };
    }
}
