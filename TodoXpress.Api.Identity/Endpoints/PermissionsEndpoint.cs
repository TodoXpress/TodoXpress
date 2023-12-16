using Carter;
using TodoXpress.Api.Identity.Entities;

namespace TodoXpress.Api.Identity.Endpoints;

public class PermissionsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        // Maps role endpoints
        var roles = app.MapGroup("roles");
        MapCRUD<Role>(roles);
        // Map endpoints for manage permissions of a role
        var perm = roles.MapGroup("{roleId:guid}/permission/{id:guid}");
        perm.MapPut("", AssignPermission);
        perm.MapDelete("", RemovePermission);

        // map permission endpoints
        var permissions = app.MapGroup("permissions");
        MapCRUD<Permission>(permissions);

        // map ressource endpoints
        var ressources = app.MapGroup("ressources");
        MapCRUD<Ressource>(ressources);

        // map scope endpoints
        var scopes = app.MapGroup("scopes");
        MapCRUD<Scope>(scopes);
    }

    public IEndpointRouteBuilder MapCRUD<TType>(IEndpointRouteBuilder route)
    {
        route.MapGet("", GetAll<TType>);
        route.MapGet("{id:guid}", Get<TType>);
        route.MapPut("", Create<TType>);
        route.MapPost("", Update<TType>);
        route.MapDelete("", Delete<TType>);

        return route;
    }

    public IResult AssignPermission(Guid id)
    {
        return Results.Ok();
    }

    public IResult RemovePermission(Guid id)
    {
        return Results.Ok();
    }

    public IResult Get<T>()
    {
        return Results.Ok();
    }

    public IResult GetAll<T>()
    {
        return Results.Ok();
    }

    public IResult Create<T>()
    {
        return Results.Ok();
    }

    public IResult Update<T>()
    {
        return Results.Ok();
    }

    public IResult Delete<T>()
    {
        return Results.Ok();
    }
}
