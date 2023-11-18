using InStock.Common.Core.Extensions;
using InStock.Frontend.Abstraction.Services.Alerts;
using InStock.Frontend.Abstraction.Services.Threading;

namespace InStock.Frontend.Mobile.Services.Alerts
{
    public class MauiAlertService : IAlertService
	{
        private readonly IMainThreadDispatcher _dispatcher;

        public MauiAlertService(IMainThreadDispatcher mainThreadDispatcher)
        {
            _dispatcher = mainThreadDispatcher;
        }

        public Task ShowServiceAlert(string title, string message, string confirm)
            => _dispatcher.DispatchOnMainThreadAsync(() => Application.Current.MainPage.DisplayAlert(title, message, confirm).FireAndForgetSafeAsync());
    }
}

