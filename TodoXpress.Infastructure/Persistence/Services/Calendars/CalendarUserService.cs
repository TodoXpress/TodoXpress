using Microsoft.EntityFrameworkCore;
using TodoXpress.Application.Contracts.Services.Calendars;
using TodoXpress.Domain.Calendars;
using TodoXpress.Infastructure.Persistence.Contexts;
using TodoXpress.Infastructure.Persistence.Services.Base;
using TodoXpress.Infastructure.Persistence.Services.Calendars;

namespace TodoXpress.Infastructure;

/// <summary>
/// Implementation of the service to manage users for calendars on the persistence level.
/// </summary>
/// <param name="context">The <see cref="CalendarDbContext"/> for the entity framework.</param>
/// <param name="uow">The unit of work to save stuff to the database.</param>
internal class CalendarUserService(CalendarDbContext context, CalendarUnitOfWork uow) 
    : DataServiceBase<User>(context), ICalendarUserService
{
    /// <inheritdoc/>
    public async Task<User?> ReadSingleAsync(Guid id)
    {
        return await _set
            .AsNoTracking()
            .FirstOrDefaultAsync(c => Equals(c.Id, id));
    }

    /// <inheritdoc/>
    public async Task<Guid> CreateAsync(User entity)
    {
        await _set.AddAsync(entity);

        return entity.Id;
    }

    /// <inheritdoc/>
    public async Task<bool> UpdateAsync(Guid entityId, User newEntity)
    {
        var user = await _set.FindAsync(entityId);
        if (user is null)
            return false;

        _set.Entry(user).CurrentValues.SetValues(newEntity);
        var entity = _set.Update(user);

        return Equals(entity.State, EntityState.Modified);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(User entity)
    {
        return await DeleteAsync(entity.Id);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id)
    {
        var user = await ReadSingleAsync(id);
        if (user is null)
            return false;

        var entity = _set.Remove(user);

        return Equals(entity.State, EntityState.Deleted);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await uow.SaveChangesAsync();
    }
}
