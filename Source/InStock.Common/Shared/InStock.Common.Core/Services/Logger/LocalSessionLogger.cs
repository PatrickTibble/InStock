using InStock.Common.Abstraction.Services.Logger;
using System.Runtime.CompilerServices;

namespace InStock.Common.Core.Services.Logger
{
    public class LocalSessionLogger : ILogger
    {
        public Task LogExceptionAsync(Exception exception, [CallerMemberName] string? callerName = null)
        {
            Console.WriteLine($"Exception in {callerName}: {exception.Message}");
            return Task.CompletedTask;
        }

        public void LogInfo(string message, [CallerMemberName] string? callerName = null)
        {
            Console.WriteLine(message, callerName);
        }
    }
}
