namespace InStock.Frontend.Abstraction.Models;

public class MarginSet
{
    public double Left { get; set; }
    public double Top { get; set; }
    public double Right { get; set; }
    public double Bottom { get; set; }

    public MarginSet()
    {
        Left = 0;
        Top = 0;
        Right = 0;
        Bottom = 0;
    }

    public MarginSet(double margin)
    {
        Left = margin;
        Top = margin;
        Right = margin;
        Bottom = margin;
    }

    public MarginSet(double leftRight, double topBottom)
    {
        Left = leftRight;
        Top = topBottom;
        Right = leftRight;
        Bottom = topBottom;
    }

    public MarginSet(double left, double top, double right, double bottom)
    {
        Left = left;
        Top = top;
        Right = right;
        Bottom = bottom;
    }

    public static MarginSet Default => new MarginSet();
}
