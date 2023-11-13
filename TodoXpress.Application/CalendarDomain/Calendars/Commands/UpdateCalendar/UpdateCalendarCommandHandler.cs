using System.ComponentModel.DataAnnotations;
using FluentValidation;
using OneOf;
using TodoXpress.Application.Contracts.MediatR;
using TodoXpress.Application.Contracts.Persistence;
using TodoXpress.Application.Contracts.Persistence.Services;
using TodoXpress.Domain;
using TodoXpress.Domain.Calendars;
using TodoXpress.Domain.Common;

namespace TodoXpress.Application.CalendarDomain.Calendars.Commands.UpdateCalendar;

public class UpdateCalendarCommandHandler : IOneOfRequestHandler<UpdateCalendarCommand, UpdateCalendarResponse>
{
    readonly IValidator<UpdateCalendarCommand> validator;

    readonly ICalendarDataService calendarService;
    readonly ICalendarUnitOfWork uow;

    public UpdateCalendarCommandHandler(IValidator<UpdateCalendarCommand> validator, ICalendarDataService calendarService, ICalendarUnitOfWork uow)
    {
        this.validator = validator;
        this.calendarService = calendarService;
        this.uow = uow;
    }

    public async Task<OneOf<UpdateCalendarResponse, IError>> Handle(UpdateCalendarCommand request, CancellationToken cancellationToken)
    {
        // validation
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return new ValidationError<ValidationResult>
            {
                ValidationErrorDescription = validationResult.ToString("|")
            };
        }

        // update
        var calendar = await calendarService.ReadSingleAsync(request.CalendarId);
        if (calendar is null)
            return new ElementNotFoundError<Calendar>();


        var newColor = new Color()
        {
            Id = calendar.Color.Id,
            A = request.NewColor?.A ?? 0,
            R = request.NewColor?.R ?? 0,
            G = request.NewColor?.G ?? 0,
            B = request.NewColor?.B ?? 0,
        };

        calendar.UpdateWith(request.NewCalendarName, request.NewColor is not null ? newColor : null);

        var calendarId = await calendarService.UpdateAsync(request.CalendarId, calendar);

        // persist
        var success = await uow.SaveChangesAsync();

        //response
        return new UpdateCalendarResponse()
        {
            CalendarGuid = calendarId,
            Successful = success
        };
    }
}
