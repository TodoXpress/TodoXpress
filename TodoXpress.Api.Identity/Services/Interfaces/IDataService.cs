namespace TodoXpress.Api.Identity;

public interface IDataService<T>
{
    /// <summary>
    /// Fetches all entites of type <see cref="T"/> from the database.
    /// </summary>
    /// <returns>The entites.</returns>
    Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// Fetches a single entity of type <see cref="T"/> from the database.
    /// </summary>
    /// <param name="id">The id of the entity.</param>
    /// <param name="withTracking">A bool indicating wheather ef core shoud track the entity or not.</param>
    /// <returns></returns>
    Task<T?> GetAsync(Guid id, bool withTracking = true);

    /// <summary>
    /// Creates a new entity of type <see cref="T"/>.
    /// </summary>
    /// <param name="entity">The new entity.</param>
    /// <returns>A bool indicating wheather the operation was successful or not.</returns>
    Task<bool> CreateAsync(T entity);

    /// <summary>
    /// Updates a new entity of type <see cref="T"/>.
    /// </summary>
    /// <param name="entity">The entity with the new values.</param>
    /// <returns>A bool indicating wheather the operation was successful or not.</returns>
    Task<bool> Update(T entity);

    /// <summary>
    /// Deletes a new entity of type <see cref="T"/>.
    /// </summary>
    /// <param name="id">The id of the entity to delete.</param>
    /// <returns>A bool indicating wheather the operation was successful or not.</returns>
    Task<bool> DeleteAsync(Guid id);
}
