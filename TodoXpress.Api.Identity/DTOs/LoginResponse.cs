namespace TodoXpress.Api.Identity.DTOs;

/// <summary>
/// Represents the response from a login.
/// </summary>
public record struct LoginResponse
{
    public string Token { get; set; }

    public DateTime ExpirationTime { get; set; }

    public string RefreshToken { get; set; }
}
