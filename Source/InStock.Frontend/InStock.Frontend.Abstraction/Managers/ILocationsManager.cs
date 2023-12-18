using InStock.Common.InventoryService.Abstraction.Entities;

namespace InStock.Frontend.Abstraction.Managers
{
    public interface ILocationsManager
    {
        Task<IList<Location>> GetLocationsAsync();
    }
}
