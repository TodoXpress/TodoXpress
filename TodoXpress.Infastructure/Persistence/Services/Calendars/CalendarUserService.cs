using Microsoft.EntityFrameworkCore;
using TodoXpress.Application.Contracts.Persistence.Services;
using TodoXpress.Domain.Calendars;
using TodoXpress.Infastructure.Persistence.Contexts;
using TodoXpress.Infastructure.Persistence.Services.Base;

namespace TodoXpress.Infastructure;

public class CalendarUserService(CalendarDbContext context) : DataServiceBase<User>(context), ICalendarUserDataService
{
    public async Task<User?> ReadSingleAsync(Guid id)
    {
        return await _set
            .AsNoTracking()
            .FirstOrDefaultAsync(c => Equals(c.Id, id));
    }

    public async Task<Guid> CreateAsync(User entity)
    {
        await _set.AddAsync(entity);

        return entity.Id;
    }

    public async Task<Guid> UpdateAsync(User entity)
    {
        var user = await ReadSingleAsync(entity.Id);
        if (user is null)
            return Guid.Empty;

        _set.Entry(user).CurrentValues.SetValues(entity);
        
        return entity.Id;
    }

    public async Task<Guid> DeleteAsync(User entity)
    {
        return await DeleteAsync(entity.Id);
    }

    public async Task<Guid> DeleteAsync(Guid id)
    {
        var user = await ReadSingleAsync(id);
        if (user is null)
            return Guid.Empty;

        _set.Remove(user);

        return user.Id;
    }
}
