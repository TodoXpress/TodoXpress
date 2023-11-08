namespace TodoXpress.Domain;

/// <summary>
/// Defines an error.
/// </summary>
public interface IError
{
    /// <summary>
    /// The type of the element on wich the error occure.
    /// </summary>
    Type Type { get; }

    /// <summary>
    /// The description of the error.
    /// </summary>
    public string Description { get; }
}
