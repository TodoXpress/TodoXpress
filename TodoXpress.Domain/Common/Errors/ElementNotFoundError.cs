namespace TodoXpress.Domain;

public class ElementNotFoundError<T> : IError
{
    public string Description => $"{typeof(T).Name} not found";
}
