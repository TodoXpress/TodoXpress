﻿using Media = System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Mvc;
using Carter;
using MediatR;
using TodoXpress.Application;
using TodoXpress.Application.CalendarDomain.Calendars.Commands.CreateCalendar;
using TodoXpress.Application.CalendarDomain.Calendars.Commands.UpdateCalendar;
using TodoXpress.Application.CalendarDomain.Calendars.Querys.GetAllFromUser;
using TodoXpress.Domain.Calendars.DTO.Calendars;
using TodoXpress.Application.CalendarDomain.Calendars.Commands.DeleteCalendar;

namespace TodoXpress.Api.Data;

/// <summary>
/// An API Endpoint with Carter.
/// </summary>
public class CalendarModul : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("calendars")
            .WithDisplayName("Calendar operations");

        app.MapGet("users/{userId:Guid}/calendars", GetAllCalendarFromUser)
            .Produces<List<QueryCalendarResponseDTO>>(StatusCodes.Status200OK)
            .Produces<ErrorResponse>(StatusCodes.Status400BadRequest)
            .WithDescription("Fetching all calendars from a user")
            .WithOpenApi();

        group.MapGet("/{calendarId:Guid}", GetSingleCalendar)
            .Produces<QueryCalendarResponseDTO>(StatusCodes.Status200OK)
            .Produces<ErrorResponse>(StatusCodes.Status400BadRequest)
            .WithDescription("Fetch a single calendar")
            .WithOpenApi();

        group.MapPut("/", CreateCalendar)
            .Accepts<CreateCalendarRequestDTO>(Media.Application.Json)
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status500InternalServerError)
            .Produces<ErrorResponse>(StatusCodes.Status400BadRequest)
            .WithDescription("Creates a new calendar.")
            .WithOpenApi();

        group.MapPost("/{calendarId:Guid}", UpdateCalendar)
            .Accepts<UpdateCalendarRequestDTO>(Media.Application.Json)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError)
            .Produces<ErrorResponse>(StatusCodes.Status400BadRequest)
            .WithDescription("Updates the values of an existing calendar")
            .WithOpenApi();

        group.MapDelete("/{calendarId:Guid}", DeleteCalendar)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError)
            .Produces<ErrorResponse>(StatusCodes.Status400BadRequest)
            .WithDescription("Deletes an existing calendar")
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
    public async Task<IResult> CreateCalendar([FromServices] ISender mediatR, [FromBody] CreateCalendarRequestDTO createRequest)
    {
        var createCommand = new CreateCalendarCommand()
        {
            Name = createRequest.Name,
            Color = createRequest.Color,
            UserId = Guid.NewGuid()
        };

        var result = await mediatR.Send(createCommand);

        return result.Match(guid => Results.Created(),
            error => Results.BadRequest(ErrorResponse.Create(error)));
    }

    /// <summary>
    /// Endpoint for update an existing calendar.
    /// </summary>
    /// <param name="mediatR">DI of the mediatR sender.</param>
    /// <param name="calendarId">The id of the calendar to update.</param>
    /// <param name="updateRequest">The request for updating the calendar.</param>
    /// <returns>A http result.</returns>
    public async Task<IResult> UpdateCalendar([FromServices] ISender mediatR,[FromRoute] Guid calendarId,[FromBody] UpdateCalendarRequestDTO updateRequest)
    {
        var updateCommand = new UpdateCalendarCommand() 
        {
            CalendarId = calendarId,
            NewCalendarName = updateRequest.Name,
            NewColor = updateRequest.Color
        };

        var result = await mediatR.Send(updateCommand);

        return result.Match(
            response => response.Successful ? Results.Ok() 
                        : Results.Problem(response.CalendarGuid.ToString(), statusCode: 500),
            error => Results.BadRequest(ErrorResponse.Create(error)));
    }

    /// <summary>
    /// Endpoint for deleting an existing calendar.
    /// </summary>
    /// <param name="mediatR">DI of the mediatR sender.</param>
    /// <param name="calendarId">the id of the calendar.</param>
    /// <returns>A http result.</returns>
    public async Task<IResult> DeleteCalendar([FromServices] ISender mediatR, [FromRoute] Guid calendarId)
    {
        var deleteCommand = new DeleteCalendarCommand()
        {
            CalendarId = calendarId
        };

        var result = await mediatR.Send(deleteCommand);

        return result.Match(
            response => response.Successful ? Results.Ok() 
                        : Results.Problem(response.CalendarId.ToString(), statusCode: 500),
            error => Results.BadRequest(ErrorResponse.Create(error)));
    }
}