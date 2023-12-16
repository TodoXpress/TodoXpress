using Microsoft.AspNetCore.Identity;

namespace TodoXpress.Api.Identity.Entities;

internal class Role : IdentityRole<Guid>
{
    public List<Permission> Permissions { get; set; } = [];
}
