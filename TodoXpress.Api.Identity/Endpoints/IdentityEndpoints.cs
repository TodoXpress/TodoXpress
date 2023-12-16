using Carter;

namespace TodoXpress.Api.Identity.Endpoints;

public class IdentityEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("register", Register);
        app.MapPost("login", Login);
        app.MapPost("refresh", Refresh);

        var pwd = app.MapGroup("password");
        pwd.MapPost("forgot", ForgotPassword);
        pwd.MapPost("reset", ResetPassword);
    }

    public IResult Register()
    {
        return Results.Ok();
    }

    public IResult Login()
    {
        return Results.Ok();
    }

    public IResult Refresh()
    {
        return Results.Ok();
    }

    public IResult ForgotPassword()
    {
        return Results.Ok();
    }

    public IResult ResetPassword()
    {
        return Results.Ok();
    }
}
