using InStock.Common.InventoryService.Abstraction.Entities;
using InStock.Common.InventoryService.Abstraction.Services;
using InStock.Common.InventoryService.Abstraction.TransferObjects.Locations;
using InStock.Common.Models.Base;
using Refit;

namespace InStock.Frontend.API.Mocks
{
    public class MockLocationsService : BaseMockService, ILocationsService
    {
        public Task<Result<LocationsResponse>> GetLocationsAsync([Header("Authorization")] string accessToken, CancellationToken? token = null)
        {
            return Task.FromResult(new Result<LocationsResponse>(200, new LocationsResponse
            {
                Locations = new List<Location>
                {
                    new Location
                    {
                        Description = "webstore",
                        ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/89/Etsy_logo.svg/512px-Etsy_logo.svg.png",
                        Name = "Etsy"
                    },
                    new Location
                    {
                        Description = "webstore",
                        ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/89/Etsy_logo.svg/512px-Etsy_logo.svg.png",
                        Name = "Etsy"
                    },
                    new Location
                    {
                        Description = "webstore",
                        ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/89/Etsy_logo.svg/512px-Etsy_logo.svg.png",
                        Name = "Etsy"
                    }
                }
            }));
        }
    }
}
