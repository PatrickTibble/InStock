using InStock.Common.Abstraction.Services.Storage;
using InStock.Common.Core.Extensions;
using InStock.Frontend.Abstraction.Managers;

namespace InStock.Frontend.Core.Managers
{
    public class LocalStorageManager : IStorageManager
    {
        private readonly IStorageService _service;

        public LocalStorageManager(IStorageService service)
        {
            _service = service;
            // ensure device id
            EnsureDeviceIdAsync()
                .FireAndForgetSafeAsync();
        }

        public Task<string?> GetAccessTokenAsync()
            => _service.GetValueOrDefaultAsync<string>(Constants.Settings.AccessToken);

        public Task<string?> GetRefreshTokenAsync()
            => _service.GetValueOrDefaultAsync<string>(Constants.Settings.RefreshToken);

        public Task<bool> RemoveAccessTokenAsync()
            => _service.TryRemoveValueAsync(Constants.Settings.AccessToken);

        public Task<bool> RemoveRefreshTokenAsync()
            => _service.TryRemoveValueAsync(Constants.Settings.RefreshToken);

        public Task<bool> SetAccessTokenAsync(string? accessToken)
            => _service.TrySetValueAsync(Constants.Settings.AccessToken, accessToken);

        public Task<bool> SetRefreshTokenAsync(string? refreshToken)
            => _service.TrySetValueAsync(Constants.Settings.RefreshToken, refreshToken);

        public async Task<Guid?> GetDeviceIdAsync()
        {
            var deviceId = await _service.GetValueOrDefaultAsync<string>(Constants.Settings.DeviceId);
            if (deviceId is null)
            {
                return null;
            }
            return Guid.Parse(deviceId);
        }

        private async Task EnsureDeviceIdAsync()
        {
            var deviceId = await _service.GetValueOrDefaultAsync<string>(Constants.Settings.DeviceId);
            if (deviceId is null)
            {
                deviceId = Guid.NewGuid().ToString();
                await _service.TrySetValueAsync(Constants.Settings.DeviceId, deviceId);
            }
        }
    }
}