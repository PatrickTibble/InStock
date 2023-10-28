namespace InStock.Frontend.Abstraction.Services.Threading
{
	public interface IDispatcher
	{
		Task DispatchOnMainThreadAsync(Action action);
	}
}

