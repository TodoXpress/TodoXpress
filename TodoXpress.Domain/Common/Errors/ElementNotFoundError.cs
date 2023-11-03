
namespace TodoXpress.Domain;

public class ElementNotFoundError<T> : IError
{
    public Type Type => this.Type;

    public string Description => $"{typeof(T).Name} not found";
}
