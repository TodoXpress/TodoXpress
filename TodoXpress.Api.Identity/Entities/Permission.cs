namespace TodoXpress.Api.Identity.Entities;

internal class Permission
{
    public Guid Id { get; set; }

    public Ressource Ressource { get; set; } = new();

    public List<Scope> Scopes { get; set; } = [];
}

internal class Ressource
{
    public Guid Id { get; set; }

    public string Value { get; set; } = string.Empty;
}

internal class Scope
{
    public Guid Id { get; set; }

    public string Value { get; set; } = string.Empty;
}
