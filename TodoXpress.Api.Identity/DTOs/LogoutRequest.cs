namespace TodoXpress.Api.Identity.DTOs;

public record struct LogoutRequest
{
    public Guid UserId { get; set; }
    public Guid ClientId { get; set; }
}
