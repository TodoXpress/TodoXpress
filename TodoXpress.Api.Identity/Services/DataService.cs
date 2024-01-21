using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoXpress.Api.Identity.DTOs;
using TodoXpress.Api.Identity.Entities;
using TodoXpress.Api.Identity.Persistence;

namespace TodoXpress.Api.Identity;

internal class RoleService(IdentityContext context, RoleManager<Role> roleManager) : DataServiceBase<Role, RoleDTO>(context)
{
    public override async Task<bool> CreateAsync(RoleDTO request)
    {
        var newRole = new Role()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            NormalizedName = roleManager.KeyNormalizer.NormalizeName(request.Name),
        };

        var result = await roleManager.CreateAsync(newRole);

        return result.Succeeded;
    }

    public override async Task<bool> UpdateAsync(Guid id, RoleDTO request)
    {
        var role = await roleManager.FindByIdAsync(id.ToString());
        if (role is null)
            return false;

        role.Name = request.Name;
        role.NormalizedName = roleManager.KeyNormalizer.NormalizeName(request.Name);

        var result = await roleManager.UpdateAsync(role);

        return result.Succeeded;
    }
}

internal class PermissionService(IdentityContext context) : DataServiceBase<Permission, PermissionDTO>(context)
{
    private readonly IdentityContext context = context;

    public override async Task<bool> CreateAsync(PermissionDTO request)
    {
        var permission = new Permission()
        {
            Id = Guid.NewGuid()
        };

        foreach (var scope in request.Scopes)
        {
            var dbScope = await context.Scopes
                .Where(s => Equals(s.Value, scope))
                .FirstOrDefaultAsync();

            if (dbScope is null)
                continue;

            permission.Scopes.Add(dbScope);
        }

        var ressource = await context.Ressources
                .Where(s => Equals(s.Value, request.Ressource))
                .FirstOrDefaultAsync();

        if (ressource is null)
            return false;

        permission.Ressource = ressource;

        var result = await context.Permissions.AddAsync(permission);
        var effectedRows = await context.SaveChangesAsync();

        return result.State == EntityState.Added && effectedRows > 0;
    }

    public override async Task<bool> UpdateAsync(Guid id, PermissionDTO request)
    {
        var permission = await context.Permissions
            .Include(p => p.Ressource)
            .Include(p => p.Scopes)
            .FirstOrDefaultAsync(p => Equals(p.Id, id));

        if (permission is null)
            return false;

        if (Equals(permission.Ressource.Value, request.Ressource))
        {
            var ressource = await context.Ressources
                .Where(s => Equals(s.Value, request.Ressource))
                .FirstOrDefaultAsync();

            if (ressource is null)
            return false;

            permission.Ressource = ressource;
        }

        permission.Scopes.Clear();

        foreach (var scope in request.Scopes)
        {
            var dbScope = await context.Scopes
                .Where(s => Equals(s.Value, scope))
                .FirstOrDefaultAsync();

            if (dbScope is null)
                continue;

            permission.Scopes.Add(dbScope);
        }

        var result = context.Permissions.Update(permission);
        var effectedRows = await context.SaveChangesAsync();

        return result.State == EntityState.Modified && effectedRows > 0;
    }
}

internal class RessourceService(IdentityContext context) : DataServiceBase<Ressource, string>(context)
{
    private readonly IdentityContext context = context;

    public override async Task<bool> CreateAsync(string request)
    {
        var ressource = new Ressource()
        {
            Id = Guid.NewGuid(),
            Value = request,
        };

        var result = await context.Ressources.AddAsync(ressource);
        var effectedRows = await context.SaveChangesAsync();

        return result.State == EntityState.Modified && effectedRows > 0;
    }

    public override async Task<bool> UpdateAsync(Guid id, string request)
    {
        var ressource = await context.Ressources.FindAsync(id);
        if (ressource is null)
            return false;

        ressource.Value = request;

        var result = context.Ressources.Update(ressource);
        var effectedRows = await context.SaveChangesAsync();

        return result.State == EntityState.Modified && effectedRows > 0;
    }
}

internal class ScopeService(IdentityContext context) : DataServiceBase<Scope, string>(context)
{
    private readonly IdentityContext context = context;

    public override async Task<bool> CreateAsync(string request)
    {
        var scope = new Scope()
        {
            Id = Guid.NewGuid(),
            Value = request,
        };

        var result = await context.Scopes.AddAsync(scope);
        var effectedRows = await context.SaveChangesAsync();

        return result.State == EntityState.Modified && effectedRows > 0;
    }

    public override async Task<bool> UpdateAsync(Guid id, string request)
    {
        var scope = await context.Scopes.FindAsync(id);
        if (scope is null)
            return false;

        scope.Value = request;

        var result = context.Scopes.Update(scope);
        var effectedRows = await context.SaveChangesAsync();

        return result.State == EntityState.Modified && effectedRows > 0;
    }
}
