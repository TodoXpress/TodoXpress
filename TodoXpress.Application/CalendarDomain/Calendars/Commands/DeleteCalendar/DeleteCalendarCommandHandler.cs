using FluentValidation;
using FluentValidation.Results;
using OneOf;
using TodoXpress.Application.Contracts.MediatR;
using TodoXpress.Application.Contracts.Persistence.Common;
using TodoXpress.Application.Contracts.Services.Calendars;
using TodoXpress.Domain;
using TodoXpress.Domain.Calendars;

namespace TodoXpress.Application.CalendarDomain.Calendars.Commands.DeleteCalendar;

public class DeleteCalendarCommandHandler : IOneOfRequestHandler<DeleteCalendarCommand, DeleteCalendarResponse>
{
    readonly IValidator<DeleteCalendarCommand>  validator;

    readonly ICalendarService calendarService;

    public DeleteCalendarCommandHandler(IValidator<DeleteCalendarCommand> validator, ICalendarService calendarService)
    {
        this.validator = validator;
        this.calendarService = calendarService;
    }

    public async Task<OneOf<DeleteCalendarResponse, IError>> Handle(DeleteCalendarCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return new ValidationError<ValidationResult>
            {
                ValidationErrorDescription = validationResult.ToString("|")
            };
        }

        var success = await calendarService.DeleteAsync(request.CalendarId);
        
        if (success)
            success = success && await calendarService.SaveChangesAsync();

        return new DeleteCalendarResponse()
        {
            CalendarId = request.CalendarId,
            Successful = success
        };
    }
}
