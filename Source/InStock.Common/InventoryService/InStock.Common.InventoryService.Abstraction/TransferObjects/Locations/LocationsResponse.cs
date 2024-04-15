using InStock.Common.InventoryService.Abstraction.Entities;

namespace InStock.Common.InventoryService.Abstraction.TransferObjects.Locations
{
    public class LocationsResponse
    {
        public IList<Location> Locations { get; set; }
    }
}