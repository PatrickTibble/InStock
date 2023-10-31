using InStock.Frontend.Abstraction.Services.Alerts;

namespace InStock.Frontend.Mobile.Services.Alerts
{
    public class MauiAlertService : IAlertService
	{
        public Task ShowServiceAlert(string title, string message, string confirm)
            => Application.Current.MainPage.DisplayAlert(title, message, confirm);
    }
}

