
namespace TodoXpress.Domain.Calendars;

/// <summary>
/// Represents an entry in a calendar.
/// </summary>
public class CalendarEvent : IIdentifieable
{
    /// <inheritdoc/>
    public Guid Id { get; set; }

    /// <summary>
    /// The title of the event.
    /// </summary>
    public string Title { get; set; } = string.Empty;


}

