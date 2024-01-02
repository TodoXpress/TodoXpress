namespace TodoXpress.Api.Identity.DTOs;

public class LoginRequest : IdentityRequestBase
{
    public Guid ClientId { get; set; }
}
