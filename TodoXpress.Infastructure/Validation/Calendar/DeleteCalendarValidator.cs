using FluentValidation;
using TodoXpress.Application.CalendarDomain.Calendars.Commands.DeleteCalendar;

namespace TodoXpress.Infastructure;

/// <summary>
/// Validator for the <see cref="DeleteCalendarCommand"/> class.
/// </summary>
public class DeleteCalendarValidator : AbstractValidator<DeleteCalendarCommand>
{
    public DeleteCalendarValidator()
    {
        RuleFor(x => x.CalendarId).NotEmpty()
            .WithMessage("calendar id of request musst be set");
    }
}
