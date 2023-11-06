using TodoXpress.Domain.Common;

namespace TodoXpress.Domain;

public class CalendarQueryDTO
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public Color? Color { get; set; }

    public List<Guid> EventIds { get; set; } = [];
}
