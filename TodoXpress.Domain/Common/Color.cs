
namespace TodoXpress.Domain.Common;

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

    public Guid Id { get; set; }

    public int A { get; set; } = 0;

    public int R { get; set; } = 0;

    public int G { get; set; } = 0;

    public int B { get; set; } = 0;
}
