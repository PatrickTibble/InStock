using System.Runtime.CompilerServices;

namespace InStock.Common.Abstraction.Logger
{
    public class LocalSessionLogger : ILogger
    {
        public Task LogExceptionAsync(Exception exception, [CallerMemberName] string? callerName = null)
        {
            Console.WriteLine($"Exception in {callerName}: {exception.Message}");
            return Task.CompletedTask;
        }
    }
}