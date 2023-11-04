using System.ComponentModel.DataAnnotations;
using FluentValidation;
using OneOf;
using TodoXpress.Application.Contracts.MediatR;
using TodoXpress.Application.Contracts.Persistence;
using TodoXpress.Domain;
using TodoXpress.Domain.Calendars;

namespace TodoXpress.Application.CalendarDomain.Calendars.Commands.CreateCalendar;

public class CreateCalendarCommandHandler : IOneOfRequestHandler<CreateCalendarCommand, Guid>
{
    readonly IValidator<CreateCalendarCommand> _validator;
    readonly ICalendarDataService _calendarService;

    public CreateCalendarCommandHandler(IValidator<CreateCalendarCommand> validator, ICalendarDataService calendarData)
    {
        _validator = validator;
        _calendarService = calendarData;
    }

    public async Task<OneOf<Guid, IError>> Handle(CreateCalendarCommand request, CancellationToken cancellationToken)
    {
        // validate request
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return new ValidationError<ValidationResult>()
            {
                ValidationErrorDescription = validationResult.ToString("|")
            };
        }

        var calendar = new Calendar()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Color = request.Color,
            Owner = request.User
        };

        // save calendar
        var calendarId = await _calendarService.CreateAsync(calendar);
        
        if (Equals(calendarId, Guid.Empty))
            return new PersistenceError<Calendar>();

        return calendarId;
    }

}
