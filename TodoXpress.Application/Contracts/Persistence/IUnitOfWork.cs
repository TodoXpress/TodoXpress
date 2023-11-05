namespace TodoXpress.Application.Contracts.Persistence;

/// <summary>
/// Defines the service for a transactional operation on the persistence level.
/// </summary>
/// <typeparam name="T">The type of the DbContext.</typeparam>
public interface IUnitOfWork<T> where T : IDbContext
{
    public Task<bool> SaveChangesAsync();
}
