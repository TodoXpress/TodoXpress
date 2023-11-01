﻿namespace TodoXpress.Domain.Calendars;

/// <summary>
/// Represents the user entity in the calendar domain
/// </summary>
public sealed class User : Common.User
{
    /// <summary>
    /// The list of the <see cref="Calendar"/>s of the user
    /// </summary>
    public List<Calendar> Calendars { get; set; } = [];
}

