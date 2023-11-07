using FluentValidation;
using TodoXpress.Application.CalendarDomain.Calendars.Commands.CreateCalendar;

namespace TodoXpress.Infastructure.Validation.Calendar;

public class CreateCalendarValidator : AbstractValidator<CreateCalendarCommand>
{
    public CreateCalendarValidator()
    {
        RuleFor(c => c.Name).NotNull().NotEmpty()
            .WithMessage("Name can not be empty");
        RuleFor(c => c.Color).SetValidator(new ARGBColorValidator());
        RuleFor(c => c.UserId).NotEmpty()
            .WithMessage("UserId can not be empty");
    }
}
