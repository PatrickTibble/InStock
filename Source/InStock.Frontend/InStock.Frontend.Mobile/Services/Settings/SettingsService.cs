using InStock.Frontend.Abstraction.Services.Settings;

namespace InStock.Frontend.Mobile.Services.Settings
{
    public class SettingsService : ISettingsService
    {
        public Task<T> GetValueOrDefaultAsync<T>(string key, T? defaultValue = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TryRemoveValueAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TrySetValueAsync<T>(string key, T value)
        {
            throw new NotImplementedException();
        }
    }
}