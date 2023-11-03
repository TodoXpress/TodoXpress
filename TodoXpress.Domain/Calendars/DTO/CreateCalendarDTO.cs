namespace TodoXpress.Domain.Calendars.DTO;

public record struct CreateCalendarDTO
{
    public string Name { get; set; }

    public ColorDTO Color { get; set; }
}

public record struct ColorDTO
{
    public int A { get; set; }

    public int R { get; set; }

    public int G { get; set; }

    public int B { get; set; }
}
