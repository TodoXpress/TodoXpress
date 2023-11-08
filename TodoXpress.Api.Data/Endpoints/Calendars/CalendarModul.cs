using Media = System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Mvc;
using Carter;
using MediatR;
using TodoXpress.Application.CalendarDomain.Calendars.Commands.CreateCalendar;
using TodoXpress.Domain.Calendars.DTO;
using TodoXpress.Application;
using TodoXpress.Domain;

namespace TodoXpress.Api.Data;

/// <summary>
/// An API Endpoint with Carter.
/// </summary>
/// <param name="mediatR">The MediatR instance for the application.</param>
public class CalendarModul : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/calendar")
            .WithDisplayName("Calendar operations");

        group.MapGet("/", GetSingleCalendar)
            .Produces<CalendarQueryDTO>(StatusCodes.Status200OK)
            .Produces<ErrorResponse>(StatusCodes.Status400BadRequest)
            .WithDescription("Fetch a single calendar")
            .WithOpenApi();

        group.MapPost("/", CreateCalendar)
            .Accepts<CreateCalendarDTO>(Media.Application.Json)
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces<ErrorResponse>(StatusCodes.Status400BadRequest)
            .WithDescription("Creates a new calendar.")
            .WithOpenApi();
    }

    /// <summary>
    /// Endpoint for fetching a calendar.
    /// </summary>
    /// <param name="mediatR">DI of the mediatR sender.</param>
    /// <param name="calendarId">The Id of the calendar.</param>
    /// <returns>A http result.</returns>
    public async Task<IResult> GetSingleCalendar([FromServices] ISender mediatR, Guid calendarId)
    {
        var fetchQuery = new GetSingleCalendarQuery()
        {
            CalendarId = calendarId
        };

        var result = await mediatR.Send(fetchQuery);

        return result.Match(
            response => Results.Ok(new CalendarQueryDTO()
            {
                Id = response.Id,
                Name = response.Name,
                Color = response.Color,
                EventIds = response.Events.Select(e => e.Id).ToList()
            }),
            error => Results.BadRequest(ErrorResponse.Create(error)));
    }

    /// <summary>
    /// Endpoint for creating an new calendar if it doesnt allready exists.
    /// </summary>
    /// <param name="mediatR">DI of the mediatR sender.</param>
    /// <param name="createRequest">The request for creating the calendar.</param>
    /// <returns>A http result.</returns>
    public async Task<IResult> CreateCalendar([FromServices] ISender mediatR, [FromBody]CreateCalendarDTO createRequest)
    {
        var createCommand = new CreateCalendarCommand()
        {
            Name = createRequest.Name,
            Color = createRequest.Color,
            UserId = Guid.NewGuid()
        };

        var result = await mediatR.Send(createCommand);

        return result.Match(guid => Results.Created("/calendar", guid),
            error => Results.BadRequest(ErrorResponse.Create(error)));
    }
}
