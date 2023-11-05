using Microsoft.EntityFrameworkCore;
using TodoXpress.Domain.Common.Contracts;
using TodoXpress.Infastructure.Persistence.Contexts;

namespace TodoXpress.Infastructure.Persistence.Services.Base;

public abstract class DataServiceBase<T> where T : class, IIdentifieable
{
    private protected DbSet<T> _set;

    public DataServiceBase(CalendarDbContext context)
    {
        _set = context.Set<T>();
    }
}
