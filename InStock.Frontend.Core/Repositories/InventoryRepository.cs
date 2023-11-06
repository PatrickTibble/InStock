using InStock.Common.InventoryService.Abstraction.Services;
using InStock.Frontend.Abstraction.Models;
using InStock.Frontend.Abstraction.Repositories;
using InStock.Frontend.Abstraction.Services.Threading;

namespace InStock.Frontend.Core.Repositories
{
    public class InventoryRepository : IInventoryRepository
	{
        private readonly IInventoryService _inventoryService;
        private readonly CancellationToken _token;

        public InventoryRepository(
			IInventoryService inventoryService,
			ITaskCancellationService taskCancellationService)
		{
            _inventoryService = inventoryService;
            _token = taskCancellationService.GetToken();
		}

        public async Task<IEnumerable<InventoryItem>?> GetFullInventoryAsync()
        {
            var response = await _inventoryService.GetAllAsync();

            if (response.IsSuccessfulStatusCode)
            {
                return response.Items?.Select(i => new InventoryItem
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