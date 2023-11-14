namespace InStock.Frontend.Abstraction.Services.Settings
{
    /// <summary>
    /// Contract for a service that provides access to application settings.
    /// </summary>
    public interface ISettingsService
    {
        /// <summary>
        /// Try to get a value from the settings.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        Task<T?> GetValueOrDefaultAsync<T>(string key, T? defaultValue = default);

        /// <summary>
        /// Try to set a value in the settings.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<bool> TrySetValueAsync<T>(string key, T value);

        /// <summary>
        /// Try to remove a value from the settings.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> TryRemoveValueAsync(string key);
    }
}