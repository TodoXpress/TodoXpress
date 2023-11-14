﻿using Media = System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Mvc;
using Carter;
using MediatR;
using TodoXpress.Application;
using TodoXpress.Application.CalendarDomain.Calendars.Commands.CreateCalendar;
using TodoXpress.Application.CalendarDomain.Calendars.Commands.UpdateCalendar;
using TodoXpress.Application.CalendarDomain.Calendars.Querys.GetAllFromUser;
using TodoXpress.Domain.Calendars.DTO.Calendars;

namespace TodoXpress.Api.Data;

/// <summary>
/// An API Endpoint with Carter.
/// </summary>
/// <param name="mediatR">The MediatR instance for the application.</param>
public class CalendarModul : ICarterModule
{
    private static readonly string CALENDAR = "calendar";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("calendars")
            .WithDisplayName("Calendar operations");

        group.MapGet(CALENDAR, GetSingleCalendar)
            .Produces<QueryCalendarResponseDTO>(StatusCodes.Status200OK)
            .Produces<ErrorResponse>(StatusCodes.Status400BadRequest)
            .WithDescription("Fetch a single calendar")
            .WithOpenApi();

        group.MapGet("user/{userId:Guid}/calendars/", GetAllCalendarFromUser)
            .Produces<List<QueryCalendarResponseDTO>>(StatusCodes.Status200OK)
            .Produces<ErrorResponse>(StatusCodes.Status400BadRequest)
            .WithDescription("Fetching all calendars from a user")
            .WithOpenApi();

        group.MapPut(CALENDAR, CreateCalendar)
            .Accepts<CreateCalendarRequestDTO>(Media.Application.Json)
            .Produces<CreateCalendarResponseDTO>(StatusCodes.Status201Created)
            .Produces<ErrorResponse>(StatusCodes.Status400BadRequest)
            .WithDescription("Creates a new calendar.")
            .WithOpenApi();

        group.MapPost(CALENDAR, UpdateCalendar)
            .Accepts<UpdateCalendarRequestDTO>(Media.Application.Json)
            .Produces<UpdateCalendarResponseDTO>(StatusCodes.Status200OK)
            .Produces<ErrorResponse>(StatusCodes.Status400BadRequest)
            .WithDescription("Updates the values of an existing calendar")
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
            response => Results.Ok(new QueryCalendarResponseDTO()
            {
                Id = response.Calendar.Id,
                Name = response.Calendar.Name,
                Color = response.Calendar.Color,
                EventIds = response.Calendar.Events.Select(e => e.Id).ToList()
            }),
            error => Results.BadRequest(ErrorResponse.Create(error)));
    }

    /// <summary>
    /// Endpoint for fetching all calendars of a user.
    /// </summary>
    /// <param name="mediatR">DI of the mediatR sender.</param>
    /// <param name="userId">The id of the user.</param>
    /// <returns>A http result.</returns>
    public async Task<IResult> GetAllCalendarFromUser([FromServices] ISender mediatR, Guid userId)
    {
        var fetchQuery = new GetAllCalendarFromUserQuery()
        {
             UserId = userId
        };

        var result = await mediatR.Send(fetchQuery);

        return result
            .Match(
                response => Results.Ok(response.Calendars
                .Select(calendar => {
                    return new QueryCalendarResponseDTO()
                    {
                        Id = calendar.Id,
                        Name = calendar.Name,
                        Color = calendar.Color,
                        EventIds = calendar.Events.Select(e => e.Id).ToList()
                    };
                })
                .ToList()),
                error => Results.BadRequest(ErrorResponse.Create(error)));
    }

    /// <summary>
    /// Endpoint for creating an new calendar if it doesnt allready exists.
    /// </summary>
    /// <param name="mediatR">DI of the mediatR sender.</param>
    /// <param name="createRequest">The request for creating the calendar.</param>
    /// <returns>A http result.</returns>
    public async Task<IResult> CreateCalendar([FromServices] ISender mediatR, [FromBody]CreateCalendarRequestDTO createRequest)
    {
        var createCommand = new CreateCalendarCommand()
        {
            Name = createRequest.Name,
            Color = createRequest.Color,
            UserId = Guid.NewGuid()
        };

        var result = await mediatR.Send(createCommand);

        return result.Match(guid => Results.Created("/calendar", 
            new CreateCalendarResponseDTO()
            {
                CalendarId = guid,
            }),
            error => Results.BadRequest(ErrorResponse.Create(error)));
    }

    /// <summary>
    /// Endpoint for update an existing calendar.
    /// </summary>
    /// <param name="mediatR">DI of the mediatR sender.</param>
    /// <param name="updateRequest">The request for updating the calendar.</param>
    /// <returns>A http results.</returns>
    public async Task<IResult> UpdateCalendar([FromServices] ISender mediatR, [FromBody] UpdateCalendarRequestDTO updateRequest)
    {
        var updateCommand = new UpdateCalendarCommand() 
        {
            CalendarId = updateRequest.CalendarId,
            NewCalendarName = updateRequest.Name,
            NewColor = updateRequest.Color
        };

        var result = await mediatR.Send(updateCommand);

        return result.Match(response => Results.Ok(new UpdateCalendarResponseDTO()
        {
            CalendarId = response.CalendarGuid,
            Success = response.Successful,
        }),
            error => Results.BadRequest(ErrorResponse.Create(error)));
    }
}
