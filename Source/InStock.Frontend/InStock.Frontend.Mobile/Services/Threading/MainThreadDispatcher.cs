using InStock.Frontend.Abstraction.Services.Threading;

namespace InStock.Frontend.Mobile.Services.Threading
{
    public class MainThreadDispatcher : IMainThreadDispatcher
	{
        public Task DispatchOnMainThreadAsync(Action action)
        {
            if (Application.Current.Dispatcher.IsDispatchRequired)
            {
                return Application.Current.Dispatcher.DispatchAsync(action);
            }
            action?.Invoke();
            return Task.CompletedTask;
        }
    }
}