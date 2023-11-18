namespace InStock.Frontend.Abstraction.Services.Platform
{
    public interface IPlatformInfoService
    {
        string ClientName { get; }
        string ClientDescription { get; }
    }
}