using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using TodoXpress.Application.Contracts.Persistence;
using TodoXpress.Domain.Calendars;
using TodoXpress.Infastructure.Persistence.Contexts;
using TodoXpress.Infastructure.Persistence.Services;

namespace TodoXpress.Infastructure;

public sealed class CalendarService : DataServiceBase<Calendar>, ICalendarDataService
{
    public CalendarService(CalendarDbContext context) : base(context)
    {
    }

    public async Task<Calendar?> ReadSingleAsync(Guid id)
    {
        return await _set
            .AsNoTracking()
            .FirstOrDefaultAsync(c => Equals(c.Id, id));
    }

    public async Task<List<Calendar>> ReadAllFromUserAsync(User user)
    {
        var calendars = await _set
            .Where(c => Equals(c.Owner.Id, user.Id))
            .ToListAsync();
        
        return calendars;
    }

    public Task<List<Calendar>> ReadAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Guid> CreateAsync(Calendar entity)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> UpdateAsync(Calendar entity)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> DeleteAsync(Calendar entity)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
