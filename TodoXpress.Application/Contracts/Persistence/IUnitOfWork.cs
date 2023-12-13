namespace TodoXpress.Application.Contracts.Persistence;

/// <summary>
/// Defines the service for a transactional operation on the persistence level.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Saves the changes to the database.
    /// </summary>
    /// <returns><see langword="true"/> if the operation was successfull, otherwise <see langword="false"/>.</returns>
    public Task<bool> SaveChangesAsync();
}
