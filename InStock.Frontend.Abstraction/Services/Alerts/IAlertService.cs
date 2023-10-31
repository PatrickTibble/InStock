namespace InStock.Frontend.Abstraction.Services.Alerts
{
    public interface IAlertService
    {
        Task ShowServiceAlert(string title, string message, string confirm);
    }
}