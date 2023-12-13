
using TodoXpress.Domain.Common.ValueTypes;

namespace TodoXpress.Domain.Common;

/// <summary>
/// Represents an argb color.
/// </summary>
public class Color : IIdentifieable
{
    public Color()
    {
    }

    public Color(int r, int g, int b, int a)
    {
        A = a;
        R = r;
        G = g;
        B = b;
    }

    /// <summary>
    /// The identifier of the color (for entity framework).
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The alpha value.
    /// </summary>
    public int A { get; set; } = 0;

    /// <summary>
    /// The value for red.
    /// </summary>
    public int R { get; set; } = 0;

    /// <summary>
    /// The value for green.
    /// </summary>
    public int G { get; set; } = 0;

    /// <summary>
    /// The value for blue.
    /// </summary>
    public int B { get; set; } = 0;
}
