namespace TodoXpress.Api.Identity.DTOs;

/// <summary>
/// Represents the response from a login.
/// </summary>
public readonly record struct LoginResponse
{
    public string Token { get; init; }

    public DateTime ExpirationTime { get; init; }

    public string RefreshToken { get; init; }
}
