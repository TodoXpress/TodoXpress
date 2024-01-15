namespace TodoXpress.Api.Identity.Entities;

/// <summary>
/// Defines that an Object has an Id for identification
/// </summary>
public interface IIdentifieable
{
    /// <summary>
    /// The unique identifier of the object.
    /// </summary>
    public Guid Id { get; set; }
}

