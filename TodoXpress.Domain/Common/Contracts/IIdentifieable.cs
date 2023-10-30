namespace TodoXpress.Domain.Common.Contracts;


/// <summary>
/// Defines that an Object has an Id for identification
/// </summary>
public interface IIdentifieable
{
    /// <summary>
    /// The Id for identification
    /// </summary>
    public Guid Id { get; set; }
}

