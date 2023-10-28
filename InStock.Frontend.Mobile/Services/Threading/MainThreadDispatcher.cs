using InStock.Frontend.Abstraction.Services.Threading;

namespace InStock.Fontend.Mobile.Services.Threading
{
    public class MainThreadDispatcher : IMainThreadDispatcher
	{
        public Task DispatchOnMainThreadAsync(Action action)
            => MainThread.InvokeOnMainThreadAsync(action);
    }
}