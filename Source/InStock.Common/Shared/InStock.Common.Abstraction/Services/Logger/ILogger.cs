using System.Runtime.CompilerServices;

namespace InStock.Common.Abstraction.Services.Logger
{
    public interface ILogger
    {
        void LogInfo(string message, [CallerMemberName] string? callerName = null);
        Task LogExceptionAsync(Exception exception, [CallerMemberName] string? callerName = null);
    }
}