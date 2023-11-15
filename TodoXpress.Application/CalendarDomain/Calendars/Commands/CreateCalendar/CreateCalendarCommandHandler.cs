using System.ComponentModel.DataAnnotations;
using FluentValidation;
using OneOf;
using TodoXpress.Application.Contracts.MediatR;
using TodoXpress.Application.Contracts.Persistence;
using TodoXpress.Application.Contracts.Persistence.Common;
using TodoXpress.Application.Contracts.Persistence.Services;
using TodoXpress.Domain;
using TodoXpress.Domain.Calendars;

namespace TodoXpress.Application.CalendarDomain.Calendars.Commands.CreateCalendar;

public class CreateCalendarCommandHandler : IOneOfRequestHandler<CreateCalendarCommand, CreateCalendarResponse>
{
    readonly IValidator<CreateCalendarCommand> _validator;
    readonly ICreateableDataService<Calendar> _calendarService;
    readonly IReadableDataService<User> _userService;
    readonly ICalendarUnitOfWork _uow;

    public CreateCalendarCommandHandler(
        IValidator<CreateCalendarCommand> validator, 
        ICalendarDataService calendarData,
        ICalendarUserDataService userData,
        ICalendarUnitOfWork unitOfWork)
    {
        _validator = validator;
        _calendarService = calendarData;
        _userService = userData;
        _uow = unitOfWork;
    }

    public async Task<OneOf<CreateCalendarResponse, IError>> Handle(CreateCalendarCommand request, CancellationToken cancellationToken)
    {
        // validate request
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return new ValidationError<ValidationResult>()
            {
                ValidationErrorDescription = validationResult.ToString("|")
            };

        // read user from database
        var user = await _userService.ReadSingleAsync(request.UserId);
        if (user is null)
            return new ElementNotFoundError<User>();

        var rc = request.Color;
        var calendar = new Calendar()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Color = new(rc.R, rc.G, rc.B, rc.A),
            Owner = user
        };

        // create and save calendar
        var calendarId = await _calendarService.CreateAsync(calendar);
        bool saveSuccessfull = await _uow.SaveChangesAsync();

        if (Equals(calendarId, Guid.Empty) || !saveSuccessfull)
        {
            return new PersistenceError<Calendar>();
        }

        return new CreateCalendarResponse()
        {
            CalendarId = calendarId,
            Successful = true
        };
    }

}
