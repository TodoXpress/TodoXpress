using FluentValidation;
using TodoXpress.Application.CalendarDomain.Calendars.Querys.GetAllFromUser;

namespace TodoXpress.Infastructure.Validation.Calendar;

/// <summary>
/// Validator for the <see cref="GetAllCalendarFromUserQuery"/> class.
/// </summary>
public class GetAllCalendarFromUserValidator : AbstractValidator<GetAllCalendarFromUserQuery>
{
    public GetAllCalendarFromUserValidator()
    {
        RuleFor(x => x.UserId).NotEmpty()
            .WithMessage("UserId can not be empty");
    }
}
