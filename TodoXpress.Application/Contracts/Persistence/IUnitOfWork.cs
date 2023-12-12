namespace TodoXpress.Application.Contracts.Persistence;

/// <summary>
/// Defines the service for a transactional operation on the persistence level.
/// </summary>
public interface IUnitOfWork
{
    public Task<bool> SaveChangesAsync();
}
