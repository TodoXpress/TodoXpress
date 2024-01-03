using Microsoft.AspNetCore.Identity;

namespace TodoXpress.Api.Identity.Entities;

public class User : IdentityUser<Guid>
{
    public List<Role> Roles { get; set; } = [];
}
