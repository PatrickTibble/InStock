using InStock.Common.Abstraction.Services.Logger;
using System.Runtime.CompilerServices;

namespace InStock.Frontend.Mobile.Services.Logger
{
    public class DebugLogger : ILogger
    {
        public Task LogExceptionAsync(Exception exception, [CallerMemberName] string? callerName = null)
        {
            System.Diagnostics.Debug.WriteLine($"Exception in {callerName}: {exception.Message}");
            return Task.CompletedTask;
        }

        public void LogInfo(string message, [CallerMemberName] string? callerName = null)
        {
            System.Diagnostics.Debug.WriteLine(message, callerName);
        }
    }
}