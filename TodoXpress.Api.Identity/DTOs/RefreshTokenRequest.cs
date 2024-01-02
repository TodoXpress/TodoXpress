namespace TodoXpress.Api.Identity.DTOs;

public class RefreshTokenRequest
{
    public string RefreshToken { get; set; } = string.Empty;

    public Guid UserId { get; set; }

    public Guid ClientId { get; set; }
}
