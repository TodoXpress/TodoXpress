
namespace TodoXpress.Domain;

/// <summary>
/// An error that is raised when validation of an element failed.
/// </summary>
/// <typeparam name="T">The type of the element.</typeparam>
public class ValidationError<T> : IError
{
    /// <inheritdoc/>
    public Type Type => typeof(T);

    /// <summary>
    /// The error message of the failed validation.
    /// </summary>
    public string? ValidationErrorDescription { get; init; }

    /// <inheritdoc/>
    public string Description => ValidationErrorDescription ?? string.Empty;
}
