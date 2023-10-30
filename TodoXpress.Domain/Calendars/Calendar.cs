using System.Drawing;

namespace TodoXpress.Domain.Calendars;

/// <summary>
/// Represents a calendar to track events.
/// </summary>
public class Calendar : IIdentifieable
{
    /// <inheritdoc/>
    public Guid Id { get; set; }

    /// <summary>
    /// The user firendly Name of the calendar.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The color of the calendar in the ui.
    /// </summary>
    public Color Color { get; set; }

    /// <summary>
    /// The <see cref="CalendarUser"/> who owns the calendar.
    /// </summary>
    public CalendarUser Owner { get; set; }

    /// <summary>
    /// The events that belongs to an calendar.
    /// </summary>
    public List<Event> Events { get; set; } = new();
}

