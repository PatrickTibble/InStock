namespace InStock.Fontend.Mobile.Services.Threading
{
    public class ThreadDispatcher : Frontend.Abstraction.Services.Threading.IDispatcher
	{
        public Task DispatchOnMainThreadAsync(Action action)
            => MainThread.InvokeOnMainThreadAsync(action);
    }
}