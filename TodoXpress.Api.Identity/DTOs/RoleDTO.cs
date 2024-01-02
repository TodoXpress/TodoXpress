namespace TodoXpress.Api.Identity.DTOs;

public struct RoleDTO
{
    public string Name { get; set; }

    public List<PermissionDTO> Permissions { get; init; }
}
