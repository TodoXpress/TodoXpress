using FluentValidation;
using TodoXpress.Application;

namespace TodoXpress.Infastructure.Validation.Calendar;

public class GetSingleCalendarValidator : AbstractValidator<GetSingleCalendarQuery>
{
    public GetSingleCalendarValidator()
    {
        RuleFor(x => x.CalendarId).NotEmpty()
            .WithMessage("CalendarId can not be empty");
    }
}
