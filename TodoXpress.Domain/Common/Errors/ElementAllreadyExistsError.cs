namespace TodoXpress.Domain;

public class ElementAllreadyExistsError<T> : IError
{
    public Type Type => this.Type;

    public string Description => $"{typeof(T).Name} allready exists";
}
