namespace TodoXpress.Domain.Common.ValueTypes;

/// <summary>
/// represents the datatransfer object for a color.
/// </summary>
public record struct ARGBColor
{
    public int A { get; set; }

    public int R { get; set; }

    public int G { get; set; }

    public int B { get; set; }
}
