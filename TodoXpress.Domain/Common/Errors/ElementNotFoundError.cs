
namespace TodoXpress.Domain;

/// <summary>
/// An error that is raised when an element is not found.
/// </summary>
/// <typeparam name="T">The type of the element.</typeparam>
public class ElementNotFoundError<T> : IError
{
    /// <inheritdoc/>
    public Type Type => this.Type;

    /// <inheritdoc/>
    public string Description => $"{typeof(T).Name} not found";
}
