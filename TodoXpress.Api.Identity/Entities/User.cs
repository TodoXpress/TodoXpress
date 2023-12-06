using Microsoft.AspNetCore.Identity;

namespace TodoXpress.Api.Identity.Entities;

internal class User : IdentityUser<Guid>
{
    public List<Role> Roles { get; set; } = [];
}
