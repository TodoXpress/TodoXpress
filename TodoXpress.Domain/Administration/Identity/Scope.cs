namespace TodoXpress.Domain.Administration.Identity;

public readonly record struct Scope
{
    private readonly string _value;

    public Scope(string value)
        => _value = value;

    public override readonly string ToString()
        => _value;
}