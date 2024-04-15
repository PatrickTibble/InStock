using InStock.Common.Abstraction.Services.Logger;
using InStock.Common.IoC;
using InStock.Frontend.Abstraction.Models;
using InStock.Frontend.Mobile.Drawables.Base;

namespace InStock.Frontend.Mobile.Drawables
{
    public class ChartDrawable : BaseDrawable
    {
        public IList<ChartPoint> Points { get; set; } = new List<ChartPoint>();

        public Color StartColor { get; set; } = new Color(0, 0, 0);
        public Color EndColor { get; set; } = new Color(0, 0, 0);

        public override void Draw(ICanvas canvas, RectF dirtyRect)
        {
            base.Draw(canvas, dirtyRect);
            if (Points.Count == 0)
            {
                return;
            }

            canvas.StrokeSize = 4;
            canvas.SetFillPaint(new LinearGradientPaint()
            {
                StartPoint = new Point(0, 0.5),
                EndPoint = new Point(1, 0.5),
                StartColor = StartColor,
                EndColor = EndColor,
            }, dirtyRect);

            var topValue = Points.Select(p => p.Value).Max();

            using var path = new PathF(0, dirtyRect.Height);
            _ = path.ProfileAndLineTo(0, GetY(dirtyRect, Points[0], topValue));
            for (var i = 1; i < Points.Count; i++)
            {
                var point = Points[i];
                var x = (float)i / (float)(Points.Count - 1);
                _ = path.ProfileAndLineTo(x * dirtyRect.Width, GetY(dirtyRect, point, topValue));
            }
            _ = path.ProfileAndLineTo(dirtyRect.Width, dirtyRect.Height);
            path.Close();

            canvas.FillPath(path);
        }

        private float GetY(RectF rect, ChartPoint point, double topValue)
        {
            return rect.Height - rect.Height * (float)(point.Value / topValue);
        }
    }

    public static class PathFExtensions
    {
        public static PathF ProfileAndLineTo(this PathF path, float x, float y)
        {
            Resolver.Resolve<ILogger>().LogInfo("Drawing path to " + x + ", " + y);
            return path.LineTo(x, y);
        }
    }
}
