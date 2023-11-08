using TodoXpress.Domain.Common.Contracts;

namespace TodoXpress.Application.Contracts.Persistence.Common;

/// <summary>
/// Defines a service that can create an entity in the persistence.
/// </summary>
/// <typeparam name="T">The type of the object to create.</typeparam>
public interface ICreateableDataService<T> where T : IIdentifieable
{
    /// <summary>
    /// Creates an <see cref="typeof(T)"/> in the persistence.
    /// </summary>
    /// <param name="entity">The entity to persist.</param>
    /// <returns>The id of the entity.</returns>
    Task<Guid> CreateAsync(T entity);
}
