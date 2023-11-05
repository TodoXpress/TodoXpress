using System.ComponentModel.DataAnnotations;
using TodoXpress.Domain.Calendars.Enumerations;

namespace TodoXpress.Domain.Calendars;

/// <summary>
/// Represents an entry in a calendar.
/// </summary>
public sealed class CalendarEvent : IIdentifieable
{
    [Key]
    /// <inheritdoc/>
    public Guid Id { get; set; }

#region Event Informations

    /// <summary>
    /// The title of the event.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// The name of the icon of the calendar.
    /// </summary>
    public string Icon { get; set; } = string.Empty;

    /// <summary>
    /// The url to join a meeting.
    /// </summary>
    public Uri? MeetingUrl { get; set; }

    /// <summary>
    /// <see langword="true"/> if the event is for the full day, otherwise <see langword="false"/> 
    /// </summary>
    public bool IsFullDay { get; set; }

    /// <summary>
    /// The start point of the event.
    /// </summary>
    public DateTime Start { get; set; }

    /// <summary>
    /// The duration of the event.
    /// </summary>
    public TimeSpan Duration { get; set; } = TimeSpan.Zero;

    /// <summary>
    /// The type of the event.
    /// </summary>
    public EventType Type { get; set; }

    /// <summary>
    /// The type of how to display the event.
    /// </summary>
    public Timeblock ShowAs { get; set; }

#endregion

#region Serial Event

    /// <summary>
    /// <see langword="true" if the defines a serial event, otherwise <see langword="false"/>/>
    /// </summary>
    public bool IsSerialEvent { get; set; }

    /// <summary>
    /// If the event is a serial event, all informations about the serial event is stored here.
    /// </summary>
    public SerialEvent? SerialEvent { get; set; }

#endregion

#region  Attachments

    /// <summary>
    /// Notes for the event.
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Files than can be attached to an event.
    /// </summary>
    public List<FileAttachment> FileAttachments { get; set; } = [];

    // Todo:
    // - Internal Links
    // - Url
    // - Tags

#endregion

    /// <summary>
    /// The calendar where the event belongs to.
    /// </summary>
    public required Calendar Calendar { get; set; }

}

