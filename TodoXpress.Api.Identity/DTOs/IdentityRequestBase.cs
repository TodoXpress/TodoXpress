using Microsoft.AspNetCore.Identity.Data;

namespace TodoXpress.Api.Identity.DTOs;

public class IdentityRequestBase
{
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public static implicit operator RegisterRequest(IdentityRequestBase req)
        => new()
        {
            Email = req.Email,
            Password = req.Password
        };
}
