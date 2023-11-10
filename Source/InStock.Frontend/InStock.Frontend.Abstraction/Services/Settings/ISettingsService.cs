namespace InStock.Frontend.Abstraction.Services.Settings
{
    public interface ISettingsService
    {
        Task<T> GetValueOrDefaultAsync<T>(string key, T? defaultValue = default);

        Task<bool> TrySetValueAsync<T>(string key, T value);

        Task<bool> TryRemoveValueAsync(string key);
    }
}