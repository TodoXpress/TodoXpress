using Microsoft.EntityFrameworkCore;
using TodoXpress.Application.Contracts.Persistence;

namespace TodoXpress.Infastructure.Persistence.Services.Base;

public abstract class UnitOfWork<TContext>(TContext context) : IUnitOfWork where TContext : DbContext
{ 
    public async Task<bool> SaveChangesAsync()
    {
        var effectedRows = await context.SaveChangesAsync();
        return effectedRows > 0;
    }
}
