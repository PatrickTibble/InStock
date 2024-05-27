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