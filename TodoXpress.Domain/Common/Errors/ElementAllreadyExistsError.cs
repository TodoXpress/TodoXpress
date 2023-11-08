namespace TodoXpress.Domain;

/// <summary>
/// An error that is raised when an element allready exists.
/// </summary>
/// <typeparam name="T">The type of the element.</typeparam>
public class ElementAllreadyExistsError<T> : IError
{
    /// <inheritdoc/>
    public Type Type => this.Type;

    /// <inheritdoc/>
    public string Description => $"{typeof(T).Name} allready exists";
}
