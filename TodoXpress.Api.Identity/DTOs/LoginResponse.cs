namespace TodoXpress.Api.Identity;

/// <summary>
/// Represents the response from a login.
/// </summary>
public readonly record struct LoginResponse
{
    public string Token { get; init; }

    public TimeSpan ExpirationTime { get; init; }

    public string RefreshToken { get; init; }
}
