using InStock.Frontend.Abstraction.Services.Threading;

namespace InStock.Frontend.Core.Services.Threading
{
    public class TaskCancellationService : ITaskCancellationService
    {
        private CancellationTokenSource? _tokenSource;

        public void Cancel()
        {
            _tokenSource?.Cancel();
        }

        public CancellationToken GetToken()
        {
            if (_tokenSource == null || _tokenSource.IsCancellationRequested)
            {
                _tokenSource = new CancellationTokenSource();
            }
            return _tokenSource.Token;
        }
    }
}