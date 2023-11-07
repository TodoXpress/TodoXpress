using FluentValidation;
using TodoXpress.Application.CalendarDomain.Calendars.Querys.GetAllFromUser;

namespace TodoXpress.Infastructure.Validation.Calendar;

public class GetAllCalendarFromUserValidator : AbstractValidator<GetAllCalendarFromUserQuery>
{
    public GetAllCalendarFromUserValidator()
    {
        RuleFor(x => x.UserId).NotEmpty()
            .WithMessage("UserId can not be empty");
    }
}
