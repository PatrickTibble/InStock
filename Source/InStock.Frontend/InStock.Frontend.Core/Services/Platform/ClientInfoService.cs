using InStock.Frontend.Abstraction.Models;
using InStock.Frontend.Abstraction.Services.Platform;

namespace InStock.Frontend.Core.Services.Platform
{
    public class ClientInfoService : IClientInfoService
    {
        public SoftwareVersion AppVersion => new SoftwareVersion("0.0.1");
    }
}