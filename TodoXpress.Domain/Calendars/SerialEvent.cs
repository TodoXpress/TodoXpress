namespace TodoXpress.Domain.Calendars;

/// <summary>
/// Defines an event, that occures multiple times.
/// </summary>
public class SerialEvent : IIdentifieable
{
    public Guid Id { get; set; }
}