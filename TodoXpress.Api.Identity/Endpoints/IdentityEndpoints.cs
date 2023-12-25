using Carter;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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

    public async Task<IResult> RegisterAsync([FromServices] IdentityService identity, [FromBody] RegisterRequest registration, HttpContext context)
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

    public async Task<IResult> LoginAsync([FromServices] IdentityService identity, [FromBody] LoginRequest login)
    {
        var (success, user) = await identity.LoginAsync(login.Email, login.Password);

        if (!success)
        {
            return TypedResults.Problem();
        }

        

        return TypedResults.Empty;
    }

    public async Task<IResult> RefreshAsync([FromServices] IServiceProvider sp, [FromBody] RefreshRequest refreshRequest)
    {
        var timeProvider = sp.GetRequiredService<TimeProvider>();
        var bearerTokenOptions = sp.GetRequiredService<IOptionsMonitor<BearerTokenOptions>>();

        var signInManager = sp.GetRequiredService<SignInManager<User>>();
            var refreshTokenProtector = bearerTokenOptions.Get(IdentityConstants.BearerScheme).RefreshTokenProtector;
            var refreshTicket = refreshTokenProtector.Unprotect(refreshRequest.RefreshToken);

            // Reject the /refresh attempt with a 401 if the token expired or the security stamp validation fails
            if (refreshTicket?.Properties?.ExpiresUtc is not { } expiresUtc ||
                timeProvider.GetUtcNow() >= expiresUtc ||
                await signInManager.ValidateSecurityStampAsync(refreshTicket.Principal) is not User user)

            {
                return TypedResults.Challenge();
            }

            var newPrincipal = await signInManager.CreateUserPrincipalAsync(user);
            return TypedResults.SignIn(newPrincipal, authenticationScheme: IdentityConstants.BearerScheme);
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
