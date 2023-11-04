using FluentValidation;
using TodoXpress.Application.CalendarDomain.Calendars.Commands.CreateCalendar;

namespace TodoXpress.Infastructure;

public class CreateCalendarValidator : AbstractValidator<CreateCalendarCommand>
{
    public CreateCalendarValidator()
    {
        RuleFor(c => c.Name).NotNull().NotEmpty()
            .WithMessage("Name can not be empty");
        RuleFor(c => c.Color).NotNull();
        RuleFor(c => c.User).NotNull()
            .WithMessage("User musst be set");
    }
}
