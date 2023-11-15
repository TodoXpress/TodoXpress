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
    /// <returns><see langword="true"/> if the operation was successful, otherwise <see langword="false"/>.</returns>
    Task<bool> UpdateAsync(Guid entityId, T newEntity);
}
