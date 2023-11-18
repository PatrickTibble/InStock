using InStock.Frontend.Abstraction.Services.Platform;

namespace InStock.Frontend.Mobile.Services.Platforms
{
    public class PlatformInfoService : IPlatformInfoService
    {
        public string ClientName { get; } = $"[{DeviceInfo.Platform}] {DeviceInfo.Manufacturer} {DeviceInfo.Model}";

        public string ClientDescription { get; } = $"{DeviceInfo.DeviceType} {DeviceInfo.VersionString} {DeviceInfo.Idiom}";
    }
}