using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoXpress.Api.Identity.Entities;

namespace TodoXpress.Api.Identity.Persistence;

internal class IdentityContext : IdentityDbContext<User, Role, Guid>
{
    internal DbSet<Permission> Permissions { get; set; }

    internal DbSet<Ressource> Ressources { get; set; }

    internal DbSet<Scope> Scopes { get; set; }

    internal DbSet<RefreshToken> RefreshTokens { get; set; }

    public IdentityContext(DbContextOptions<IdentityContext> options) 
        : base(options)
    {
        if (this.Database.GetPendingMigrations().Any())
        {
            this.Database.Migrate();
        }
    }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Permission>()
            .HasMany(p => p.Scopes)
            .WithMany("Permissions")
            .UsingEntity(j => j.ToTable("PermissionScopes"));

        modelBuilder.Entity<Role>()
            .HasMany(r => r.Permissions)
            .WithMany("Roles")
            .UsingEntity(j => j.ToTable("RolePermissions"));
    }
}
