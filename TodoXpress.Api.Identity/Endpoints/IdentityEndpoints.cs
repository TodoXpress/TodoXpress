﻿using Carter;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoXpress.Api.Identity.DTOs;
using TodoXpress.Api.Identity.Entities;
using TodoXpress.Api.Identity.Services.Interfaces;

namespace TodoXpress.Api.Identity.Endpoints;

public class IdentityEndpoints : ICarterModule
{
    private string? confirmEmailEndpointName = null;

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("register", RegisterAsync);
        app.MapPost("login", LoginAsync);
        app.MapPost("refresh", RefreshAsync);

        var pwd = app.MapGroup("password");
        pwd.MapPost("forgot", ForgotPassword);
        pwd.MapPost("reset", ResetPassword);

        var confirmation = app.MapGroup("confirm");
        confirmation.MapGet("email", ConfirmMail)
            .Add(endpointBuilder =>
            {
                var finalPattern = ((RouteEndpointBuilder)endpointBuilder).RoutePattern.RawText;
                confirmEmailEndpointName = $"{nameof(AddRoutes)}-{finalPattern}";
                endpointBuilder.Metadata.Add(new EndpointNameMetadata(confirmEmailEndpointName));
            });
        confirmation.MapPost("resentemail", ResentConfirmation);
    }

    /// <summary>
    /// Registers a new user in the system.
    /// </summary>
    /// <param name="identity">The service to interact witch the aspnet identity services.</param>
    /// <param name="registration">The request with the data for the registration.</param>
    /// <param name="context">The <see cref="HttpContext"/> of the request.</param>
    /// <returns>An Http status result.</returns>
    /// <exception cref="NotSupportedException">Thrown when the userstore don't support mail.</exception>
    public async Task<IResult> RegisterAsync([FromServices] IIdentityService identity, [FromBody] IdentityRequestBase registration, HttpContext context)
    {
        if (!identity.SupportsUserEmail())
        {
            throw new NotSupportedException($"{nameof(RegisterAsync)} requires a user store with email support.");
        }

        // creates the user
        var (createUserResult, user) = await identity.CreateUserAsync(registration);
        if (!createUserResult.Succeeded)
        {
            return this.CreateValidationProblem(createUserResult);
        }

        // add user to Role
        var addRoleResult = await identity.AddUserToDefaultRoleAsync(user!);
        if (!addRoleResult.Succeeded)
        {
            return this.CreateValidationProblem(addRoleResult);
        }

        // sends confirmation mail
        await identity.SendConfirmationEmailAsync(user!, context, confirmEmailEndpointName!, user!.Email!);

        return TypedResults.Ok();
    }

    /// <summary>
    /// Logs a user in.
    /// </summary>
    /// <param name="identity">The service to interact witch the aspnet identity services.</param>
    /// <param name="token">The service to create tokens.</param>
    /// <param name="login">The request with the data.</param>
    /// <returns>An Http status result.</returns>
    public async Task<IResult> LoginAsync(
        [FromServices] IIdentityService identity, 
        [FromServices] ITokenService token, 
        [FromBody] LoginRequest login)
    {
        var (success, user) = await identity.LoginAsync(login.Email, login.Password);

        if (!success)
            return TypedResults.Problem("Login fehlgeschlagen.");

        var loginResponse = await token.CreateAuthTokenForUser(user!, login.ClientId);

        return TypedResults.Ok(loginResponse);
    }

    /// <summary>
    /// Refreshes the auth token with a refresh token
    /// </summary>
    /// <param name="ITokenService">The service to create tokens.</param>
    /// <param name="userManager">The service to interact with user entites.</param>
    /// <param name="refreshRequest">The request with the data.</param>
    /// <returns>An Http status result.</returns>
    public async Task<IResult> RefreshAsync(
        [FromServices] ITokenService ITokenService, 
        [FromServices] UserManager<User> userManager,
        [FromBody] RefreshTokenRequest refreshRequest)
    {
        bool isValidToken = await ITokenService.ValidateRefreshTokenAsync(refreshRequest.RefreshToken, refreshRequest.UserId, refreshRequest.ClientId);
    
        if (!isValidToken)
            return TypedResults.Unauthorized();

        var user = await userManager.FindByIdAsync(refreshRequest.UserId.ToString());
        if (user is null)
            return TypedResults.Unauthorized();

        var newLogin = await ITokenService.RefreshAuthTokenForUser(refreshRequest.RefreshToken, user, refreshRequest.ClientId);
        return TypedResults.Ok(newLogin);
    }

    /// <summary>
    /// Sends a forgot password mail.
    /// </summary>
    /// <param name="identity">The service to interact with </param>
    /// <param name="forgotRequest">The request with the data.</param>
    /// <returns>An Http status result.</returns>
    public async Task<IResult> ForgotPassword(
        [FromServices] IIdentityService identity,
        [FromBody] Microsoft.AspNetCore.Identity.Data.ForgotPasswordRequest forgotRequest)
    {
        var result = await identity.SendPasswordForgotEmailAsync(forgotRequest.Email);

        if (!result.Succeeded)
            return this.CreateValidationProblem(result);

        return TypedResults.Ok();
    }

    public async Task<IResult> ResetPassword(
        [FromServices] IIdentityService identity, 
        [FromBody] Microsoft.AspNetCore.Identity.Data.ResetPasswordRequest resetRequest)
    {
        var result = await identity.ResetPasswordAsync(resetRequest.Email, resetRequest.ResetCode, resetRequest.NewPassword);
        
        if (!result.Succeeded)
            return this.CreateValidationProblem(result);

        return TypedResults.Ok();
    }

    public IResult ConfirmMail()
    {
        return Results.Ok();
    }

    public IResult ResentConfirmation()
    {
        return Results.Ok();
    }
}
