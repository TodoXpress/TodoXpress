using FluentValidation;
using TodoXpress.Application.CalendarDomain.Calendars.Commands.UpdateCalendar;
using TodoXpress.Infastructure.Validation.Common;

namespace TodoXpress.Infastructure.Validation.Calendar;

public class UpdateCalendarValidator : AbstractValidator<UpdateCalendarCommand>
{
    /// <summary>
    /// Validator for the <see cref="UpdateCalendarCommand"/> class.
    /// </summary>
    public UpdateCalendarValidator()
    {
        RuleFor(x => x.CalendarId).NotEmpty()
            .WithMessage("calendar id of request musst be set");
        RuleFor(x => x.NewColor)
            .SetValidator(new NullableARGBColorValidator());
    }
}
