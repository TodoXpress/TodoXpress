using FluentValidation;
using FluentValidation.Results;
using OneOf;
using TodoXpress.Application.Contracts.MediatR;
using TodoXpress.Application.Contracts.Persistence;
using TodoXpress.Application.Contracts.Persistence.Common;
using TodoXpress.Application.Contracts.Persistence.Services;
using TodoXpress.Domain;
using TodoXpress.Domain.Calendars;

namespace TodoXpress.Application.CalendarDomain.Calendars.Commands.DeleteCalendar;

public class DeleteCalendarCommandHandler : IOneOfRequestHandler<DeleteCalendarCommand, DeleteCalendarResponse>
{
    readonly IValidator<DeleteCalendarCommand>  validator;

    readonly IDeleteableDataService<Calendar> calendarService;

    readonly ICalendarUnitOfWork uow;

    public DeleteCalendarCommandHandler(IValidator<DeleteCalendarCommand> validator, ICalendarDataService calendarService, ICalendarUnitOfWork uow)
    {
        this.validator = validator;
        this.calendarService = calendarService;
        this.uow = uow;
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
            success = success && await uow.SaveChangesAsync();

        return new DeleteCalendarResponse()
        {
            CalendarId = request.CalendarId,
            Successful = success
        };
    }
}
