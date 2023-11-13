namespace InStock.Frontend.Abstraction.Services.Alerts
{
    /// <summary>
    /// Contract for alert services.
    /// </summary>
    public interface IAlertService
    {
        /// <summary>
        /// Show an alert with a title, message, and confirm button.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="confirm"></param>
        /// <returns></returns>
        Task ShowServiceAlert(string title, string message, string confirm);
    }
}