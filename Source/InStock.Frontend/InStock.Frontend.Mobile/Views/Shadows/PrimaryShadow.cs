namespace InStock.Frontend.Mobile.Views.Shadows
{
    public class PrimaryShadow : Shadow
    {
        public PrimaryShadow()
        {
            Radius = 8;
            Brush = new SolidColorBrush(Color.FromArgb("#FF000000"));
            Offset = new Point(2, 6);
            Opacity = 0.25f;
        }
    }
}