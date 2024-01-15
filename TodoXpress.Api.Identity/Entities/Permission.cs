namespace TodoXpress.Api.Identity.Entities;

public class Permission : IIdentifieable
{
    public Guid Id { get; set; }

    public Ressource Ressource { get; set; } = new();

    public List<Scope> Scopes { get; set; } = [];
}

public class Ressource : IIdentifieable
{
    public Guid Id { get; set; }

    public string Value { get; set; } = string.Empty;
}

public class Scope : IIdentifieable
{
    public Guid Id { get; set; }

    public string Value { get; set; } = string.Empty;
}
