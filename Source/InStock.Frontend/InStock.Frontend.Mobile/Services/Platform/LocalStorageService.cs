using InStock.Common.Abstraction.Services.Logger;
using InStock.Common.Abstraction.Services.Storage;
using InStock.Common.Core.Extensions;

namespace InStock.Frontend.Mobile.Services.Platform
{
    public class LocalStorageService : IStorageService
    {
        private readonly ILogger _logger;

        public LocalStorageService(ILogger logger)
        {
            _logger = logger;
        }

        public Task<T?> GetValueOrDefaultAsync<T>(string key, T? defaultValue = default)
        {
            var result = defaultValue;
            try
            {
                result = Preferences.Default.Get(key, defaultValue);
            }
            catch (Exception e)
            {
                _logger.LogExceptionAsync(e).FireAndForgetSafeAsync();
            }
            return ValueTask.FromResult(result).AsTask();
        }

        public Task<bool> TryRemoveValueAsync(string key)
        {
            try
            {
                if (Preferences.Default.ContainsKey(key))
                {
                    Preferences.Default.Remove(key);
                }
            }
            catch (Exception e)
            {
                _logger.LogExceptionAsync(e).FireAndForgetSafeAsync();
            }
            return new ValueTask<bool>(true).AsTask();
        }

        public Task<bool> TrySetValueAsync<T>(string key, T value)
        {
            try
            {
                Preferences.Default.Set(key, value);
            }
            catch (Exception e)
            {
                _logger.LogExceptionAsync(e).FireAndForgetSafeAsync();
            }
            return new ValueTask<bool>(true).AsTask();
        }
    }
}
