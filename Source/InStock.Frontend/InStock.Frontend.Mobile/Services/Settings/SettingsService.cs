using InStock.Frontend.Abstraction.Services.Settings;

namespace InStock.Frontend.Mobile.Services.Settings
{
    public class SettingsService : ISettingsService
    {

        public Task<T> GetValueOrDefaultAsync<T>(string key, T? defaultValue = default)
            => Task.FromResult(Preferences.Default.Get(key, defaultValue));

        public Task<bool> TryRemoveValueAsync(string key)
        {
            Preferences.Default.Remove(key);
            return Task.FromResult(true);
        }

        public Task<bool> TrySetValueAsync<T>(string key, T value)
        {
            Preferences.Default.Set(key, value);
            return Task.FromResult(true);
        }
    }
}