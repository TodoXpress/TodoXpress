﻿using Carter;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoXpress.Api.Identity.Entities;
using TodoXpress.Api.Identity.Services.Interfaces;

namespace TodoXpress.Api.Identity.Endpoints;

public class AccountEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var acc = app.MapGroup("accounts/{id:guid}")
            .RequireAuthorization();

        acc.MapGet("", GetInfos)
            .Produces(StatusCodes.Status401Unauthorized);
        acc.MapPost("", UpdateInfos)
            .Produces(StatusCodes.Status401Unauthorized);

        acc.MapDelete("", DeleteUserAsync)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .ProducesValidationProblem()
            .WithOpenApi();

        var perm = acc.MapGroup("roles");
        perm.MapPut("{roleId:guid}", AssignRoleAsync)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithOpenApi();

        perm.MapDelete("{roleId:guid}", RemoveRoleAsync)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithOpenApi();
    }

    /// <summary>
    /// Return informations about the account.
    /// </summary>
    /// <returns>An Http status result.</returns>
    public static IResult GetInfos()
    {
        return Results.Ok();
    }

    /// <summary>
    /// Updates informations of the account.
    /// </summary>
    /// <returns>An Http status result.</returns>
    public IResult UpdateInfos()
    {
        return Results.Ok();
    }

    /// <summary>
    /// Deletes an User-Account and all data that belongs to it.
    /// </summary>
    /// <param name="identity">The service to interact witch the aspnet identity services.</param>
    /// <param name="userId">The id of the user to delete.</param>
    /// <returns>An Http status result.</returns>
    public async Task<IResult> DeleteUserAsync([FromServices] IIdentityService identity, [FromRoute] Guid id)
    {
        var result = await identity.DeleteUserAsync(id);

        if(!result.Succeeded)
            this.CreateValidationProblem(result);

        return TypedResults.Ok();
    }

    /// <summary>
    /// Adds a user to a role.
    /// </summary>
    /// <param name="userManager">The service for the users.</param>
    /// <param name="roleManager">The service for the roles.</param>
    /// <param name="roleId">The identifier of the role.</param>
    /// <param name="id">The identifier of the user.</param>
    /// <returns>An Http status result.</returns>
    public async Task<IResult> AssignRoleAsync(
        [FromServices] UserManager<User> userManager,
        [FromServices] RoleManager<Role> roleManager,
        [FromRoute] Guid roleId, 
        [FromRoute] Guid id)
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

    /// <summary>
    /// Removes a user from a role.
    /// </summary>
    /// <param name="userManager">The service for the users.</param>
    /// <param name="roleManager">The service for the roles.</param>
    /// <param name="roleId">The identifier of the role.</param>
    /// <param name="id">The identifier of the user.</param>
    /// <returns>An Http status result.</returns>
    public async Task<IResult> RemoveRoleAsync(
        [FromServices] UserManager<User> userManager,
        [FromServices] RoleManager<Role> roleManager,
        [FromRoute] Guid roleId, 
        [FromRoute] Guid id
    )
    {
        var user = await userManager.FindByIdAsync(id.ToString());
        if (user is null)
        {
            return TypedResults.NotFound("User not found.");
        }

        var role = await roleManager.FindByIdAsync(roleId.ToString());
        if (role is null)
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
