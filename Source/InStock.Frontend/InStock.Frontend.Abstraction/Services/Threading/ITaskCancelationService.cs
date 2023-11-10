namespace InStock.Frontend.Abstraction.Services.Threading
{
	public interface ITaskCancellationService
	{
		CancellationToken GetToken();
		void Cancel();
	}
}

