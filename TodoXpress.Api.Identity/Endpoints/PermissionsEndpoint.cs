using Carter;
using TodoXpress.Api.Identity.Entities;

namespace TodoXpress.Api.Identity.Endpoints;

public class PermissionsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        // Maps role endpoints
        var roles = app.MapGroup("roles");
        roles.MapCRUD<Role>();
        // Map endpoints for manage permissions of a role
        var perm = roles.MapGroup("{roleId:guid}/permission/{id:guid}");
        roles.MapPut("", AssignPermission);
        roles.MapDelete("", RemovePermission);

        // map permission endpoints
        var permissions = app.MapGroup("permissions");
        permissions.MapCRUD<Permission>();

        // map ressource endpoints
        var ressources = app.MapGroup("ressources");
        ressources.MapCRUD<Ressource>();

        // map scope endpoints
        var scopes = app.MapGroup("scopes");
        scopes.MapCRUD<Scope>();
    }

    public IResult AssignPermission(Guid id)
    {
        return Results.Ok();
    }

    public IResult RemovePermission(Guid id)
    {
        return Results.Ok();
    }

}

public static class EndpointExtention
{
    public static IEndpointRouteBuilder MapCRUD<TType>(this IEndpointRouteBuilder route)
    {
        route.MapGet("", CRUDEndpoints<Role>.GetAll);
        route.MapGet("{id:guid}", CRUDEndpoints<Role>.Get);
        route.MapPut("", CRUDEndpoints<Role>.Create);
        route.MapPost("", CRUDEndpoints<Role>.Update);
        route.MapDelete("", CRUDEndpoints<Role>.Delete);

        return route;
    }
}

public static class CRUDEndpoints<T>
{
    public static IResult Get()
    {
        return Results.Ok();
    }

    public static IResult GetAll()
    {
        return Results.Ok();
    }

    public static IResult Create()
    {
        return Results.Ok();
    }

    public static IResult Update()
    {
        return Results.Ok();
    }

    public static IResult Delete()
    {
        return Results.Ok();
    }
}
