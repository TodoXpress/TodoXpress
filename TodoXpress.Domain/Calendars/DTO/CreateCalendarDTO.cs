using TodoXpress.Domain.Common.ValueTypes;

namespace TodoXpress.Domain.Calendars.DTO;

public record struct CreateCalendarDTO
{
    public string Name { get; set; }

    public ARGBColor Color { get; set; }
}
