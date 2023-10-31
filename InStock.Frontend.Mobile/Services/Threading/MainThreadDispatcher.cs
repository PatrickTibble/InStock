using InStock.Frontend.Abstraction.Services.Threading;

namespace InStock.Frontend.Mobile.Services.Threading
{
    public class MainThreadDispatcher : IMainThreadDispatcher
	{
        public Task DispatchOnMainThreadAsync(Action action)
            => MainThread.InvokeOnMainThreadAsync(action);
    }
}