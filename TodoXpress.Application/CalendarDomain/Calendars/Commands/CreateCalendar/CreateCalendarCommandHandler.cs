using System.ComponentModel.DataAnnotations;
using System.Drawing;
using FluentValidation;
using OneOf;
using TodoXpress.Application.Contracts.MediatR;
using TodoXpress.Application.Contracts.Persistence;
using TodoXpress.Application.Contracts.Persistence.Services;
using TodoXpress.Domain;
using TodoXpress.Domain.Calendars;

namespace TodoXpress.Application.CalendarDomain.Calendars.Commands.CreateCalendar;

public class CreateCalendarCommandHandler : IOneOfRequestHandler<CreateCalendarCommand, Guid>
{
    readonly IValidator<CreateCalendarCommand> _validator;
    readonly ICalendarDataService _calendarService;
    readonly ICalendarUnitOfWork _uow;

    public CreateCalendarCommandHandler(IValidator<CreateCalendarCommand> validator, ICalendarDataService calendarData, ICalendarUnitOfWork unitOfWork)
    {
        _validator = validator;
        _calendarService = calendarData;
        _uow = unitOfWork;
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

        var rc = request.Color;
        var calendar = new Calendar()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Color = new(rc.R, rc.G, rc.B, rc.A),
            Owner = request.User
        };

        // save calendar
        var calendarId = await _calendarService.CreateAsync(calendar);
        
        if (Equals(calendarId, Guid.Empty))
        {
            await _uow.SaveChangesAsync();
            return new PersistenceError<Calendar>();
        }

        return calendarId;
    }

}
