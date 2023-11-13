using TodoXpress.Domain.Calendars;
using TodoXpress.Domain.Common;

namespace TodoXpress.Application.CalendarDomain.Calendars.Commands.UpdateCalendar;

/// <summary>
/// Extention Methods for updating a Calendar.
/// </summary>
public static class CalendarExtentions
{
    public static void UpdateWith(this Calendar calendar, string? name, Color? color)
    {
        calendar.Name = name is null ? calendar.Name : name;
        calendar.Color = color is null ? calendar.Color : color;
    }
}
