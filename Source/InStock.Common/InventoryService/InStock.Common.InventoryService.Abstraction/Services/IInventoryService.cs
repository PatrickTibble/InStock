using InStock.Common.InventoryService.Abstraction.TransferObjects.Delete;
using InStock.Common.InventoryService.Abstraction.TransferObjects.Get;
using InStock.Common.InventoryService.Abstraction.TransferObjects.GetAll;
using InStock.Common.InventoryService.Abstraction.TransferObjects.Insert;
using InStock.Common.Models.Base;
using Refit;

namespace InStock.Common.InventoryService.Abstraction.Services
{
    public interface IInventoryService
    {
        [Post("/api/v1/Inventory")]
        Task<Result<InsertResponse>> AddOrUpdateAsync(InsertRequest request);

        [Delete("/api/v1/Inventory/{id}")]
        Task<Result<DeleteResponse>> DeleteAsync(int id);

        [Get("/api/v1/Inventory")]
        Task<Result<GetAllResponse>> GetAllAsync();

        [Get("/api/v1/Inventory/{id}")]
        Task<Result<GetResponse>> GetAsync(int id);
    }
}