using Carter;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        perm.MapPut("", AssignPermissionAsync);
        perm.MapDelete("", RemovePermissionAsync);

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
        route.MapGet("", GetAllAsync<TType>);
        route.MapGet("{id:guid}", GetAsync<TType>);
        route.MapPut("", CreateAsync<TType>);
        route.MapPost("", UpdateAsync<TType>);
        route.MapDelete("{id:guid}", DeleteAsync<TType>);

        return route;
    }

    public async Task<IResult> AssignPermissionAsync(
        [FromServices] RoleManager<Role> roleManager,
        [FromServices] IDataService<Permission> permissions,
        [FromRoute] Guid roleId,
        [FromRoute] Guid id)
    {
        var role = await roleManager.FindByIdAsync(roleId.ToString());
        if (role is null)
            return TypedResults.BadRequest("role identifier is invalid");

        var permission = await permissions.GetAsync(id);
        if (permission is null)
            return TypedResults.BadRequest("permission identifier is invalid");

        role.Permissions.Add(permission);
        await roleManager.UpdateAsync(role);

        return TypedResults.Ok();
    }

    public async Task<IResult> RemovePermissionAsync(
        [FromServices] RoleManager<Role> roleManager,
        [FromServices] IDataService<Permission> permissions,
        [FromRoute] Guid roleId,
        [FromRoute] Guid id
    )
    {
        var role = await roleManager.FindByIdAsync(roleId.ToString());
        if (role is null)
            return TypedResults.BadRequest("role identifier is invalid");

        var permission = await permissions.GetAsync(id);
        if (permission is null)
            return TypedResults.BadRequest("permission identifier is invalid");

        role.Permissions.Remove(permission);
        await roleManager.UpdateAsync(role);

        return TypedResults.Ok();
    }

    public async Task<IResult> GetAllAsync<T>([FromServices] IDataService<T> dataService)
    {
        return TypedResults.Ok(await dataService.GetAllAsync());
    }

    public async Task<IResult> GetAsync<T>([FromServices] IDataService<T> dataService, [FromRoute] Guid id)
    {
        var result = await dataService.GetAsync(id);
        if (result is null)
            return TypedResults.NotFound();

        return TypedResults.Ok(result);
    }

    public async Task<IResult> CreateAsync<T>([FromServices] IDataService<T> dataService, [FromBody] T entity)
    {
        bool success = await dataService.CreateAsync(entity);
        if (!success)
            return TypedResults.StatusCode(StatusCodes.Status500InternalServerError);
        
        return TypedResults.Ok();
    }

    public async Task<IResult> UpdateAsync<T>([FromServices] IDataService<T> dataService, [FromBody] T entity)
    {
        bool success = await dataService.Update(entity);
        if (!success)
            return TypedResults.StatusCode(StatusCodes.Status500InternalServerError);

        return Results.Ok();
    }

    public async Task<IResult> DeleteAsync<T>([FromServices] IDataService<T> dataService, [FromRoute] Guid id)
    {
        bool success = await dataService.DeleteAsync(id);
        if (!success)
            return TypedResults.StatusCode(StatusCodes.Status500InternalServerError);

        return Results.Ok();
    }
}
