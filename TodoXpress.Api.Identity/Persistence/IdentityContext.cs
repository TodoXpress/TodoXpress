using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoXpress.Api.Identity.Entities;

namespace TodoXpress.Api.Identity.Persistence;

public class IdentityContext : IdentityDbContext<User>
{
    public IdentityContext(DbContextOptions<IdentityContext> options) 
        : base(options)
    {
    }
}
