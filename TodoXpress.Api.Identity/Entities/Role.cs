using System.Reflection.Metadata;
using Microsoft.AspNetCore.Identity;

namespace TodoXpress.Api.Identity.Entities;

public class Role : IdentityRole<Guid>
{
    public const string DefaultUserRole = "default_user";

    public const string PayingUserRole = "pro_user";

    public const string AdminUser = "admin_user";

    public List<Permission> Permissions { get; set; } = [];
}
