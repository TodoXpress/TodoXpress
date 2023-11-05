namespace TodoXpress.Domain.Common;

public class Color
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

    public int A { get; set; } = 0;

    public int R { get; set; } = 0;

    public int G { get; set; } = 0;

    public int B { get; set; } = 0;
}
