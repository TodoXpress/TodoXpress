using Carter;

namespace TodoXpress.Api.Identity.Endpoints;

public class AccountEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var acc = app.MapGroup("accounts/{id:guid}");
        acc.MapGet("", GetInfos);
        acc.MapPost("", UpdateInfos);

        var perm = acc.MapGroup("role");
        perm.MapPut("{roleId:guid}", AssignRole);
        perm.MapDelete("{roleId:guid}", RemoveRole);
    }

    public static IResult GetInfos()
    {
        return Results.Ok();
    }

    public IResult UpdateInfos()
    {
        return Results.Ok();
    }

    public IResult AssignRole(Guid roleId)
    {
        return Results.Ok();
    }

    public IResult RemoveRole(Guid roleId)
    {
        return Results.Ok();
    }
}
