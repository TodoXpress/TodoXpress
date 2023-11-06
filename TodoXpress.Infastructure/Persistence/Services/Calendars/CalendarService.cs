using Microsoft.EntityFrameworkCore;
using TodoXpress.Application.Contracts.Persistence.Services;
using TodoXpress.Domain.Calendars;
using TodoXpress.Infastructure.Persistence.Contexts;
using TodoXpress.Infastructure.Persistence.Services.Base;

namespace TodoXpress.Infastructure.Persistence.Services.Calendars;

public sealed class CalendarService(CalendarDbContext context) 
    : DataServiceBase<Calendar>(context), ICalendarDataService
{
    public async Task<Calendar?> ReadSingleAsync(Guid id)
    {
        return await _set
            .AsNoTracking()
            .FirstOrDefaultAsync(c => Equals(c.Id, id));
    }

    public async Task<List<Calendar>> ReadAllFromUserAsync(User user)
    {
        var calendars = await _set
            .AsNoTracking()
            .Where(c => Equals(c.Owner.Id, user.Id))
            .ToListAsync();
        
        return calendars;
    }

    public async Task<Guid> CreateAsync(Calendar entity)
    {
        await _set.AddAsync(entity);

        return entity.Id;
    }

    public async Task<Guid> UpdateAsync(Calendar entity)
    {
        var calendar = await ReadSingleAsync(entity.Id);
        if (calendar is null)
            return Guid.Empty;

        _set.Entry(calendar).CurrentValues.SetValues(entity);

        return entity.Id;
    }

    public async Task<Guid> DeleteAsync(Calendar entity)
    {
        return await DeleteAsync(entity.Id);
    }

    public async Task<Guid> DeleteAsync(Guid id)
    {
        var calendar = await ReadSingleAsync(id);
        if (calendar is null)
            return Guid.Empty;

        _set.Remove(calendar);

        return calendar.Id;
    }
}
