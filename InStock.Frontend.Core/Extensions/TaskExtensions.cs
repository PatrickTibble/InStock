using System;
namespace InStock.Frontend.Core.Extensions
{
	public static class TaskExtensions
	{
#pragma warning disable S3168 // "async" methods should not return "void"
        public static async void FireAndForgetSafeAsync(this Task task, Action<Exception>? exceptionHandler = null, bool configureAwait = false)
#pragma warning restore S3168 // "async" methods should not return "void"
        {
			try
			{
				await task.ConfigureAwait(configureAwait);
			}
			catch (Exception e)
			{
				exceptionHandler?.Invoke(e);
			}
		}
	}
}

