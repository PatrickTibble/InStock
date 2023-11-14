using InStock.Common.Core.Extensions;
using InStock.Frontend.Abstraction.Managers;
using InStock.Frontend.Abstraction.Services.Settings;

namespace InStock.Frontend.Core.Managers
{
    public class SettingsManager : ISettingsManager
    {
        private readonly ISettingsService _service;

        public SettingsManager(ISettingsService service)
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

        public Task<Guid?> GetDeviceIdAsync()
            => _service.GetValueOrDefaultAsync<Guid?>(Constants.Settings.DeviceId);

        private async Task EnsureDeviceIdAsync()
        {
            var deviceId = await _service.GetValueOrDefaultAsync<Guid?>(Constants.Settings.DeviceId);
            if (deviceId is null)
            {
                deviceId = Guid.NewGuid();
                await _service.TrySetValueAsync(Constants.Settings.DeviceId, deviceId);
            }
        }
    }
}