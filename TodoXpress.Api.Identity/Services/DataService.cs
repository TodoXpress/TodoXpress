using Microsoft.EntityFrameworkCore;
using TodoXpress.Api.Identity.Entities;
using TodoXpress.Api.Identity.Persistence;

namespace TodoXpress.Api.Identity;

internal class DataService<T>(IdentityContext context) : IDataService<T> where T : class, IIdentifieable
{
    private readonly DbSet<T> set = context.Set<T>();

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await this.set
            .ToListAsync();
    }

    public async Task<T?> GetAsync(Guid id, bool withTracking = true)
    {
        T? entity; 
        
        if (withTracking)
            entity = await this.set
                .FindAsync(id);
        else
            entity = await this.set
                .AsNoTracking()
                .FirstOrDefaultAsync(e => Equals(id, e.Id));

        return entity;
    }

    public async Task<bool> CreateAsync(T entity)
    {
        var existingEntity = await this.GetAsync(entity.Id, false);
        if (existingEntity is not null)
            return false;

        await set.AddAsync(entity);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Update(T entity)
    {
        var existingEntity = await this.GetAsync(entity.Id, false);
        if (existingEntity is null)
            return false;

        context.Entry(existingEntity).CurrentValues.SetValues(entity);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var existingEntity = await this.GetAsync(id, false);
        if (existingEntity is null)
            return false;

        this.set.Remove(existingEntity);
        await context.SaveChangesAsync();

        return true;
    }
}
