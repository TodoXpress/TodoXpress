namespace TodoXpress.Domain.Administration.Identity;

public class Role
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public List<Permission> Permissions { get; set; } = [];

    private Role(string name)
        => Name = name;
}
