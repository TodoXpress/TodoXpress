using Microsoft.EntityFrameworkCore;
using TodoXpress.Api.Identity.Entities;
using TodoXpress.Api.Identity.Persistence;

namespace TodoXpress.Api.Identity;

internal abstract class DataServiceBase<T, TRequest>(IdentityContext context) : IDataService<T, TRequest> where T : class, IIdentifieable
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

    public abstract Task<bool> CreateAsync(TRequest request);

    public abstract Task<bool> UpdateAsync(Guid id, TRequest request);

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
