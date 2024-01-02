namespace TodoXpress.Api.Identity.Entities;

internal class RefreshToken
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }

    public Guid ClientId { get; set; }
    
    public string Token { get; set; } = string.Empty;
    
    public DateTime CreationDate { get; set; }
    
    public DateTime ExpiryDate { get; set; }
}
