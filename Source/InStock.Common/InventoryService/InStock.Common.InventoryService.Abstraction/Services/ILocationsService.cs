using InStock.Common.InventoryService.Abstraction.TransferObjects.Locations;
using InStock.Common.Models.Base;
using Refit;

namespace InStock.Common.InventoryService.Abstraction.Services
{
    public interface ILocationsService
    {
        [Get($"/{Constants.Locations}")]
        Task<Result<LocationsResponse>> GetLocations([Header(Constants.Authorization)] string accessToken, CancellationToken? token = null);
    }
}