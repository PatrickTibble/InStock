namespace InStock.Common.Abstraction.Services.Downloading
{
    public interface IDownloadingService
    {
        Task<byte[]> DownloadAsync(string url, CancellationToken? token = null);
    }
}
