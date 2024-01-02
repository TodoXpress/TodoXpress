namespace TodoXpress.Api.Identity.DTOs;

public readonly record struct PermissionDTO
{
    public string Ressource { get; init; }

    public List<string> Scopes { get; init; }
}
