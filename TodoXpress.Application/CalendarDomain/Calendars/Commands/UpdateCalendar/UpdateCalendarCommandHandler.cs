using System.ComponentModel.DataAnnotations;
using FluentValidation;
using OneOf;
using TodoXpress.Application.Contracts.MediatR;
using TodoXpress.Application.Contracts.Services.Calendars;
using TodoXpress.Domain;
using TodoXpress.Domain.Calendars;
using TodoXpress.Domain.Common;

namespace TodoXpress.Application.CalendarDomain.Calendars.Commands.UpdateCalendar;

public class UpdateCalendarCommandHandler : IOneOfRequestHandler<UpdateCalendarCommand, UpdateCalendarResponse>
{
    readonly IValidator<UpdateCalendarCommand> validator;

    readonly ICalendarService calendarService;

    public UpdateCalendarCommandHandler(IValidator<UpdateCalendarCommand> validator, ICalendarService calendarService)
    {
        this.validator = validator;
        this.calendarService = calendarService;
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

        calendarService.UpdateValues(calendar, request.NewCalendarName, newColor);

        var success = await calendarService.UpdateAsync(request.CalendarId, calendar);
        
        if (success)
            success = success && await calendarService.SaveChangesAsync();

        //response
        return new UpdateCalendarResponse()
        {
            CalendarGuid = request.CalendarId,
            Successful = success
        };
    }
}
