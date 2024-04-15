using System.Drawing;

namespace InStock.Frontend.Abstraction.Models
{
    public class Axis
    {
        public IList<string> Names { get; set; }
    }

    public class Axes
    {
        public Axis X { get; set; }
        public Axis Y { get; set; }
    }

    public class ChartPoint
    {
        public string Name { get; set; }
        public double Value { get; set; }

        public ChartPoint(double val, string? name = null)
        {
            Value = val;
            Name = name ?? string.Empty;
        }
    }

    public class ChartDataSet
    {
        public IList<ChartPoint> Points { get; set; }
        public Axes Axis { get; set; }
    }
}