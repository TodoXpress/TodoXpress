
namespace TodoXpress.Domain;

/// <summary>
/// An error that is raised when something went wrong while persisting an entity.
/// </summary>
/// <typeparam name="T">The type of the element.</typeparam>
public class PersistenceError<T> : IError
{
    /// <inheritdoc/>
    public Type Type => typeof(T);

    /// <inheritdoc/>
    public string Description => $"Could not persist {Type.Name}";
}
