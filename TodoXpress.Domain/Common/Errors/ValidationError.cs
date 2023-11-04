
namespace TodoXpress.Domain;

public class ValidationError<T> : IError
{
    public Type Type => typeof(T);

    public string? ValidationErrorDescription { get; init; }
    public string Description => ValidationErrorDescription ?? string.Empty;
}
