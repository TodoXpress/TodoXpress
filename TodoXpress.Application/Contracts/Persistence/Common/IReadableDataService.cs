using TodoXpress.Domain.Common.Contracts;

namespace TodoXpress.Application.Contracts.Persistence.Common;

/// <summary>
/// Defines a service that can query an entity from the persistence.
/// </summary>
/// <typeparam name="T">The type of the object to query.</typeparam>
public interface IReadableDataService<T> where T : IIdentifieable
{
    /// <summary>
    /// Reads an <see cref="typeof(T)"/> from the persistence.
    /// </summary>
    /// <param name="entity">The id of the entity to query.</param>
    /// <returns>The entity or null.</returns>
    Task<T?> ReadSingleAsync(Guid id);
}
