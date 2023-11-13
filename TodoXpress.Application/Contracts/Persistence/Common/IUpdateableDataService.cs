using TodoXpress.Domain.Common.Contracts;

namespace TodoXpress.Application.Contracts.Persistence.Common;

/// <summary>
/// Defines a service that can update an entity in the persistence.
/// </summary>
/// <typeparam name="T">The type of the object to update.</typeparam>
public interface IUpdateableDataService<T> where T : IIdentifieable
{
    /// <summary>
    /// Updates an <see cref="typeof(T)"/> in the persistence.
    /// </summary>
    /// <param name="entityId">The id of the entity to update.</param>
    /// <param name="newEntity">The entity with new values.</param>
    /// <returns>The id of the entity.</returns>
    Task<Guid> UpdateAsync(Guid entityId, T newEntity);
}
