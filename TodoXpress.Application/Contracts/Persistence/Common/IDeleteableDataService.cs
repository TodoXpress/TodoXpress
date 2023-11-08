using TodoXpress.Domain.Common.Contracts;

namespace TodoXpress.Application.Contracts.Persistence.Common;

/// <summary>
/// Defines a service that can delete an entity in the persistence.
/// </summary>
/// <typeparam name="T">The type of the object to delete.</typeparam>
public interface IDeleteableDataService<T> where T : IIdentifieable
{
    /// <summary>
    /// Deletes an <see cref="typeof(T)"/> in the persistence.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <returns>The id of the entity.</returns>
    Task<Guid> DeleteAsync(T entity);

    /// <summary>
    /// Deletes an <see cref="typeof(T)"/> in the persistence.
    /// </summary>
    /// <param name="entity">The id of the entity to delete.</param>
    /// <returns>The id of the entity.</returns>
    Task<Guid> DeleteAsync(Guid id);
}
