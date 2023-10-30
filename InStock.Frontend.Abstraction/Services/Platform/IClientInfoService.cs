using InStock.Frontend.Abstraction.Models;

namespace InStock.Frontend.Abstraction.Services.Platform
{
    public interface IClientInfoService
    {
        SoftwareVersion AppVersion { get; }
    }
}