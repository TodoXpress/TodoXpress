using Microsoft.EntityFrameworkCore;
using TodoXpress.Application.Contracts.Persistence.Services;
using TodoXpress.Domain.Calendars;
using TodoXpress.Infastructure.Persistence.Contexts;
using TodoXpress.Infastructure.Persistence.Services.Base;

namespace TodoXpress.Infastructure.Persistence.Services.Calendars;

/// <summary>
/// Implementation of the service to manage calendars on the persistence level.
/// </summary>
/// <param name="context">The <see cref="CalendarDbContext"/> for the entity framework.</param>
public sealed class CalendarService(CalendarDbContext context) 
    : DataServiceBase<Calendar>(context), ICalendarDataService
{
    /// <inheritdoc/>
    public async Task<Calendar?> ReadSingleAsync(Guid id)
    {
        return await _set
            .AsNoTracking()
            .AsSplitQuery()
            .Include(c => c.Events
                .Select(e => e.Id))
            .FirstOrDefaultAsync(c => Equals(c.Id, id));
    }

    /// <inheritdoc/>
    public async Task<List<Calendar>> ReadAllFromUserAsync(User user)
    {
        var calendars = await _set
            .AsNoTracking()
            .AsSplitQuery()
            .Include(c => c.Events
                .Select(e => e.Id))
            .Where(c => Equals(c.Owner.Id, user.Id))
            .ToListAsync();
        
        return calendars;
    }

    /// <inheritdoc/>
    public async Task<Guid> CreateAsync(Calendar entity)
    {
        await _set.AddAsync(entity);

        return entity.Id;
    }

    /// <inheritdoc/>
    public async Task<Guid> UpdateAsync(Guid entityId, Calendar newEntity)
    {
        var calendar = await _set.FindAsync(entityId);
        if (calendar is null)
            return Guid.Empty;

        _set.Entry(calendar).CurrentValues.SetValues(newEntity);

        return entityId;
    }

    /// <inheritdoc/>
    public async Task<Guid> DeleteAsync(Calendar entity)
    {
        return await DeleteAsync(entity.Id);
    }

    /// <inheritdoc/>
    public async Task<Guid> DeleteAsync(Guid id)
    {
        var calendar = await ReadSingleAsync(id);
        if (calendar is null)
            return Guid.Empty;

        _set.Remove(calendar);

        return calendar.Id;
    }
}
