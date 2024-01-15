using Carter;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoXpress.Api.Identity.Entities;

namespace TodoXpress.Api.Identity.Endpoints;

public class AccountEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var acc = app.MapGroup("accounts/{id:guid}");
        acc.MapGet("", GetInfos);
        acc.MapPost("", UpdateInfos);

        var perm = acc.MapGroup("role");
        perm.MapPut("{roleId:guid}", AssignRoleAsync);
        perm.MapDelete("{roleId:guid}", RemoveRoleAsync);
    }

    public static IResult GetInfos()
    {
        return Results.Ok();
    }

    public IResult UpdateInfos()
    {
        return Results.Ok();
    }

    public async Task<IResult> AssignRoleAsync(
        [FromServices] UserManager<User> userManager,
        [FromServices] RoleManager<Role> roleManager,
        Guid roleId, 
        Guid id)
    {
         var user = await userManager.FindByIdAsync(id.ToString());
        if (user is null)
            return TypedResults.NotFound("User not found.");

        var role = await roleManager.FindByIdAsync(roleId.ToString());
        if (role is null)
            return TypedResults.NotFound("Role not found.");

        var result = await userManager.AddToRoleAsync(user, role.Name!);
        if (!result.Succeeded)
            return TypedResults.StatusCode(StatusCodes.Status500InternalServerError);

        return TypedResults.Ok();
    }

    public async Task<IResult> RemoveRoleAsync(
        [FromServices] UserManager<User> userManager,
        [FromServices] RoleManager<Role> roleManager,
        Guid roleId, 
        Guid id
    )
    {
        var user = await userManager.FindByIdAsync(id.ToString());
        if (user is null)
        {
            return TypedResults.NotFound("User not found.");
        }

        var role = await roleManager.FindByIdAsync(roleId.ToString());
        if (role == null)
        {
            return TypedResults.NotFound("Role not found.");
        }

        var result = await userManager.RemoveFromRoleAsync(user, role.Name!);
        if (!result.Succeeded)
        {
            return TypedResults.StatusCode(StatusCodes.Status500InternalServerError);
        }

        return TypedResults.Ok();
    }
}
