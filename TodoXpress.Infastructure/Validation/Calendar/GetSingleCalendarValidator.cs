using FluentValidation;
using TodoXpress.Application;

namespace TodoXpress.Infastructure.Validation.Calendar;

/// <summary>
/// Validator for the <see cref="GetSingleCalendarQuery"/> class.
/// </summary>
public class GetSingleCalendarValidator : AbstractValidator<GetSingleCalendarQuery>
{
    public GetSingleCalendarValidator()
    {
        RuleFor(x => x.CalendarId).NotEmpty()
            .WithMessage("CalendarId can not be empty");
    }
}
