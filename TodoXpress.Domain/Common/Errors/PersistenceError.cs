
namespace TodoXpress.Domain;

public class PersistenceError<T> : IError
{
    public Type Type => typeof(T);

    public string Description => $"Could not persist {Type.Name}";
}
