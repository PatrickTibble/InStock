namespace InStock.Frontend.Abstraction.Services.Threading
{
	public interface IMainThreadDispatcher
	{
		Task DispatchOnMainThreadAsync(Action action);
	}
}

