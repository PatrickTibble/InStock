using InStock.Common.Abstraction.Services.Logger;
using InStock.Common.Core.Extensions;
using InStock.Common.InventoryService.Abstraction.Entities;
using InStock.Common.InventoryService.Abstraction.Services;
using InStock.Frontend.Abstraction.Managers;
using InStock.Frontend.Core.Extensions;

namespace InStock.Frontend.Core.Managers
{
    public class LocationsManager : ILocationsManager
    {
        private readonly ILogger _logger;
        private readonly ILocationsService _locationService;
        private readonly IStorageManager _storageManager;

        public LocationsManager(
            ILocationsService locationsService,
            ILogger logger,
            IStorageManager storageManager)
        {
            _logger = logger;
            _locationService = locationsService;
            _storageManager = storageManager;
        }

        public async Task<IList<Location>> GetLocationsAsync()
        {
            try
            {
                var token = new CancellationTokenSource().Token;
                var accessToken = await _storageManager.GetAccessTokenAsync();
                var locationsResult = await _locationService.GetLocationsAsync(accessToken!, token);

                if (locationsResult.IsSuccessfulStatusCode()
                    && locationsResult.Data != null)
                {
                    return locationsResult.Data.Locations;
                }
            }
            catch (Exception e)
            {
                _logger.LogExceptionAsync(e).FireAndForgetSafeAsync();
            }

            return new List<Location>();
        }
    }
}
