using InStock.Common.IoC;

namespace InStock.Frontend.Mobile.Services;

public class ServiceHelper : IServiceHelper
{
    public IServiceProvider Provider
        => IPlatformApplication.Current.Services;
}
