namespace TodoXpress.Api.Identity.Entities;

public class Permission
{
    public Guid Id { get; set; }

    public Ressource Ressource { get; set; } = new();

    public List<Scope> Scopes { get; set; } = [];
}

public class Ressource
{
    public Guid Id { get; set; }

    public string Value { get; set; } = string.Empty;
}

public class Scope
{
    public Guid Id { get; set; }

    public string Value { get; set; } = string.Empty;
}
