namespace TodoXpress.Domain.Calendars;

/// <summary>
/// Defines an event, that occures multiple times.
/// </summary>
public class SerialEvent : IIdentifieable
{
    /// <inheritdoc/>
    public Guid Id { get; set; }

    /// <summary>
    /// The lenght between the serial events.
    /// </summary>
    public SerialEventType Itterations { get; set; }

    /// <summary>
    /// The end of the series.
    /// </summary>
    public DateOnly EndOfSeries { get; set; }

    /// <summary>
    /// The Days when the serial event shout be set.
    /// </summary>
    public Day MultiDaySerialEvent { get; set; }

    /// <summary>
    /// The event in a calender that the series belongs to.
    /// </summary>
    public required CalendarEvent Event { get; set; }
}