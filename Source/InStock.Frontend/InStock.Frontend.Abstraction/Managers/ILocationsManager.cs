using InStock.Frontend.Abstraction.Models;

namespace InStock.Frontend.Abstraction.Managers
{
    public interface ILocationsManager
    {
        Task<IList<Location>> GetLocationsAsync();
    }
}
