using System.ComponentModel.DataAnnotations;
using TodoXpress.Domain.Common;

namespace TodoXpress.Domain.Calendars;

/// <summary>
/// Represents a calendar to track events.
/// </summary>
public sealed class Calendar : IIdentifieable
{
    [Key]
    /// <inheritdoc/>
    public Guid Id { get; set; }

    /// <summary>
    /// The user firendly Name of the calendar.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The color of the calendar in the ui.
    /// </summary>
    public Color Color { get; set; } = new();

    /// <summary>
    /// The <see cref="CalendarUser"/> who owns the calendar.
    /// </summary>
    public required User Owner { get; set; }

    /// <summary>
    /// The events that belongs to an calendar.
    /// </summary>
    public List<CalendarEvent> Events { get; set; } = [];
}

