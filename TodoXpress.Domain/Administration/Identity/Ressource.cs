namespace TodoXpress.Domain.Administration.Identity;

public record struct Ressource
{
    private readonly string _value;

    public Guid Id { get; set; }

    public Ressource(string value)
        => _value = value;

    public override readonly string ToString()
        => _value;
}
