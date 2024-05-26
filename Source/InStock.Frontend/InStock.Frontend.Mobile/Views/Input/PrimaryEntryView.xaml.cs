namespace InStock.Frontend.Mobile.Views.Input;

public partial class PrimaryEntryView : ContentView
{
    public static readonly BindableProperty IconProperty =
        BindableProperty.Create(
            propertyName: nameof(Icon),
            returnType: typeof(string),
            declaringType: typeof(PrimaryEntryView),
            defaultValue: default(string),
            propertyChanged: (b, o, n) => ((PrimaryEntryView)b).OnIconPropertyChanged((string)o, (string)n));

    public string Icon
    {
        get => (string)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public PrimaryEntryView()
    {
        InitializeComponent();

        var colors = new Color[]
        {
            Color.FromArgb("#F5F5F5"),
            Color.FromArgb("#E0E0E0"),
            Color.FromArgb("#BDBDBD"),
            Color.FromArgb("#9E9E9E"),
            Color.FromArgb("#757575"),
            Color.FromArgb("#616161"),
            Color.FromArgb("#424242"),
        };

        var rnd = new Random();
        BackgroundColor = colors[rnd.Next() % colors.Length];
    }

    private void OnIconPropertyChanged(string oldValue, string newValue)
    {
        if (oldValue == newValue)
        {
            return;
        }

        if (newValue == null)
        {
            mIcon.IsVisible = false;
            return;
        }
        mIcon.IsVisible = true;
        mIcon.Source = newValue;
    }
}