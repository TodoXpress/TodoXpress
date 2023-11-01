namespace TodoXpress.Domain.Calendars;

/// <summary>
/// Defines an event, that occures multiple times.
/// </summary>
public class SerialEvent : IIdentifieable
{
    /// <inheritdoc/>
    public Guid Id { get; set; }
}