using InStock.Common.Abstraction.Services.Downloading;
using InStock.Common.Abstraction.Services.Logger;
using InStock.Common.Core.Extensions;
using System.Net;

namespace InStock.Common.Core.Services.Downloading
{
    public class DownloadingService : IDownloadingService
    {
        private readonly ILogger _logger;

        public DownloadingService(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<byte[]> DownloadAsync(string url, CancellationToken? token = null)
        {
            try
            {
                using HttpClient client = new();
                var byteArray = await client.GetByteArrayAsync(url).ConfigureAwait(false);

                return byteArray;
            }
            catch (Exception e)
            {
                _logger.LogExceptionAsync(e).FireAndForgetSafeAsync();
            }

            return Array.Empty<byte>();
        }
    }
}
