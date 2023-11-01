
namespace TodoXpress.Domain.Common;

/// <summary>
/// Represents the base class of all user entities.
/// </summary>
public abstract class User : IIdentifieable
{
    /// <inheritdoc/>
    public Guid Id { get; set; }

    /// <summary>
    /// The unique name of the user.
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// The name to display it or share with other users.
    /// </summary>
    public string DisplayName {  get; set; } = string.Empty;
}
