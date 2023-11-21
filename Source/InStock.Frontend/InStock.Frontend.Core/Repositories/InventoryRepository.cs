using InStock.Common.InventoryService.Abstraction.Services;
using InStock.Frontend.Abstraction.Models;
using InStock.Frontend.Abstraction.Repositories;
using InStock.Frontend.Abstraction.Services.Threading;
using InStock.Frontend.Core.Extensions;

namespace InStock.Frontend.Core.Repositories
{
    public class InventoryRepository : IInventoryRepository
	{
        private readonly IInventoryService _inventoryService;

        public InventoryRepository(
			IInventoryService inventoryService)
		{
            _inventoryService = inventoryService;
		}

        public async Task<IEnumerable<InventoryItem>?> GetFullInventoryAsync()
        {
            var response = await _inventoryService.GetAllAsync();

            if (response.IsSuccessfulStatusCode() && response.Data != null)
            {
                return response.Data.Items?.Select(i => new InventoryItem
                {
                    Id = i.Id,
                    Name = i.Name,
                    Description = i.Description,
                }) ?? new List<InventoryItem>();
            }

            return default;
        }
    }
}