namespace TodoXpress.Domain;

public class ElementAllreadyExistsError<T> : IError
{
    public string Description => $"{typeof(T).Name} allready exists";
}
