namespace TodoXpress.Domain;

/// <summary>
/// Defines the day of the week.
/// </summary>
[Flags]
public enum Day
{
    Monday = 1,
    Tuesday = 2,
    Wednesday = 4,
    Thursday = 8,
    Friday = 16,
    Saturday = 32,
    Sunday = 64
}
