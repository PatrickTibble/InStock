using CommunityToolkit.Maui.Converters;
using InStock.Common.Abstraction.Services.Downloading;
using InStock.Common.Abstraction.Services.Logger;
using InStock.Common.Core.Extensions;
using InStock.Common.IoC;
using InStock.Frontend.Abstraction.Services.Threading;

namespace InStock.Frontend.Mobile.Views.Images;

public partial class CachedImage
{
    public static readonly BindableProperty SourceProperty = BindableProperty.Create(
        propertyName: nameof(Source),
        returnType: typeof(string),
        declaringType: typeof(CachedImage),
        defaultValue: null,
        propertyChanged: (b, o, n) => ((CachedImage)b).OnSourcePropertyChanged((string)n));

    public static readonly BindableProperty ShowLoadingProperty = BindableProperty.Create(
        propertyName: nameof(ShowLoading),
        returnType: typeof(bool),
        declaringType: typeof(CachedImage),
        defaultValue: true);

    public bool ShowLoading
    {
        get => (bool)GetValue(ShowLoadingProperty);
        set => SetValue(ShowLoadingProperty, value);
    }

    public string Source
    {
        get => (string)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    public CachedImage()
    {
        InitializeComponent();
    }

    private void OnSourcePropertyChanged(string newValue)
    {
        if (string.IsNullOrWhiteSpace(newValue))
        {
            // set to null (unless we have a default image..)
            image.Source = null;
            return;
        }

        try
        {
            if (newValue.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                var downloadService = Resolver.Resolve<IDownloadingService>();
                var threadService = Resolver.Resolve<IMainThreadDispatcher>();

                activityIndicator.IsRunning = activityIndicator.IsVisible = ShowLoading;
                Task.Factory.StartNew(async () =>
                {
                    var data = await downloadService.DownloadAsync(newValue);
                    if (data != null)
                    {
                        MemoryStream memory = new(data);
                        var imageSource = ImageSource.FromStream(() => memory);
                        await threadService.DispatchOnMainThreadAsync(() =>
                        {
                            image.Source = imageSource;
                            activityIndicator.IsRunning = activityIndicator.IsVisible = false;
                        });
                    }
                }).FireAndForgetSafeAsync();
            }
            else
            {
                image.Source = newValue;
            }
        }
        catch (Exception e)
        {
            Resolver.Resolve<ILogger>().LogExceptionAsync(e).FireAndForgetSafeAsync();
        }
    }
}
