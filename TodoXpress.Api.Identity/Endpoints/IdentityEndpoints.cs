using Carter;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TodoXpress.Api.Identity.DTOs;
using TodoXpress.Api.Identity.Entities;
using TodoXpress.Api.Identity.Services;

namespace TodoXpress.Api.Identity.Endpoints;

internal class IdentityEndpoints : ICarterModule
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

    public async Task<IResult> RegisterAsync([FromServices] IdentityService identity, [FromBody] IdentityRequestBase registration, HttpContext context)
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

    public async Task<IResult> LoginAsync(
        [FromServices] IdentityService identity, 
        [FromServices] TokenService token, 
        [FromBody] LoginRequest login)
    {
        var (success, user) = await identity.LoginAsync(login.Email, login.Password);

        if (!success)
            return TypedResults.Problem("Login fehlgeschlagen.");

        var loginResponse = await token.CreateAuthTokenForUser(user!, login.ClientId);

        return TypedResults.Ok(loginResponse);
    }

    public async Task<IResult> RefreshAsync(
        [FromServices] TokenService tokenService, 
        [FromServices] UserManager<User> userManager,
        [FromBody] RefreshTokenRequest refreshRequest)
    {
        bool isValidToken = await tokenService.ValidateRefreshTokenasync(refreshRequest.RefreshToken, refreshRequest.UserId, refreshRequest.ClientId);
    
        if (!isValidToken)
            return TypedResults.Unauthorized();

        var user = await userManager.FindByIdAsync(refreshRequest.UserId.ToString());
        if (user is null)
            return TypedResults.Unauthorized();

        var newLogin = await tokenService.RefreshAuthTokenForUser(refreshRequest.RefreshToken, user, refreshRequest.ClientId);
        return TypedResults.Ok(newLogin);
    }

    public IResult ForgotPassword()
    {
        return Results.Ok();
    }

    public IResult ResetPassword()
    {
        return Results.Ok();
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
