using InStock.Common.InventoryService.Abstraction.TransferObjects.Delete;
using InStock.Common.InventoryService.Abstraction.TransferObjects.Get;
using InStock.Common.InventoryService.Abstraction.TransferObjects.GetAll;
using InStock.Common.InventoryService.Abstraction.TransferObjects.Insert;
using Refit;

namespace InStock.Common.InventoryService.Abstraction.Services
{
    public interface IInventoryService
    {
        [Post("/api/v1/Inventory")]
        Task<InsertResponse> AddOrUpdateAsync(InsertRequest request);

        [Delete("/api/v1/Inventory/{id}")]
        Task<DeleteResponse> DeleteAsync(int id);

        [Get("/api/v1/Inventory")]
        Task<GetAllResponse> GetAllAsync();

        [Get("/api/v1/Inventory/{id}")]
        Task<GetResponse> GetAsync(int id);
    }
}