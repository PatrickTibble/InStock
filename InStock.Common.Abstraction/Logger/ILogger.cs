using System.Runtime.CompilerServices;

namespace InStock.Common.Abstraction.Logger
{
    public interface ILogger
    {
        Task LogExceptionAsync(Exception exception, [CallerMemberName] string? callerName = null);
    }
}