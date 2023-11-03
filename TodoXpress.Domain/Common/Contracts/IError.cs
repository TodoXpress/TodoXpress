namespace TodoXpress.Domain;

public interface IError
{
    Type Type { get; }

    public string Description { get; }
}
