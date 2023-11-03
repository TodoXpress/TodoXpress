using System.Drawing;
using Media = System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Mvc;
using Carter;
using MediatR;
using TodoXpress.Application.CalendarDomain.Calendars.Commands.CreateCalendar;
using TodoXpress.Domain.Calendars.DTO;

namespace TodoXpress.Api.Data;

/// <summary>
/// An API Endpoint with Carter module.
/// </summary>
/// <param name="mediatR">The MediatR instance for the application.</param>
public class CalendarModul(ISender mediatR) : ICarterModule
{
    private readonly ISender _mediatR = mediatR;

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/calendar");
        group.MapPut("/", CreateCalendar)
            .Accepts<CreateCalendarDTO>(Media.Application.Json)
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces<ErrorResponse>(StatusCodes.Status400BadRequest)
            .WithDisplayName("Create Calendar.")
            .WithDescription("Creates a new calendar.")
            .WithOpenApi();
    }

    /// <summary>
    /// Endpoint for creating an new calendar if it doesnt allready exists.
    /// </summary>
    /// <param name="createRequest">The request for creating the calendar.</param>
    /// <returns>A http result.</returns>
    public async Task<IResult> CreateCalendar([FromBody]CreateCalendarDTO createRequest)
    {
        var createCommand = new CreateCalendarCommand()
        {
            Name = createRequest.Name,
            Color = Color.FromArgb(createRequest.Color.A, 
                createRequest.Color.R,
                createRequest.Color.G,
                createRequest.Color.B)
        };

        var result = await _mediatR.Send(createCommand);

        return result.Match(guid => Results.Created("/calendar", guid),
            error => Results.BadRequest(ErrorResponse.Create(error)));
    }
}
