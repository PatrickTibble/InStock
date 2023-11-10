using InStock.Common.InventoryService.Abstraction.TransferObjects.Get;
using InStock.Common.InventoryService.Abstraction.TransferObjects.GetAll;
using InStock.Common.InventoryService.Abstraction.TransferObjects.Delete;
using InStock.Common.InventoryService.Abstraction.TransferObjects.Insert;
using InStock.Common.InventoryService.Abstraction.Services;

namespace InStock.Frontend.API.Mocks
{
    public class MockInventoryService : BaseMockService, IInventoryService
	{
        private IList<MockInventoryItem> _inventoryItems = new List<MockInventoryItem>()
        {
            new MockInventoryItem(0, "Test inventory item 1", "Test inventory item description. Lorem ipsum"),
            new MockInventoryItem(1, "Test inventory item 2", "Test inventory item description. Lorem ipsum"),
            new MockInventoryItem(2, "Test inventory item 3", "Test inventory item description. Lorem ipsum"),
            new MockInventoryItem(3, "Test inventory item 4", "Test inventory item description. Lorem ipsum"),
            new MockInventoryItem(4, "Test inventory item 5", "Test inventory item description. Lorem ipsum"),
            new MockInventoryItem(5, "Test inventory item 6", "Test inventory item description. Lorem ipsum")
        };

        public Task<InsertResponse> AddOrUpdateAsync(InsertRequest request)
        {
            return Delay()
                .ContinueWith(_ => new InsertResponse
                {

                });
        }

        public Task<DeleteResponse> DeleteAsync(int id)
        {
            return Delay()
                .ContinueWith(_ => new DeleteResponse
                {

                });
        }

        public Task<GetAllResponse> GetAllAsync()
        {
            var model = new GetAllResponse
            {
                Items = new List<GetResponse>(),
                IsSuccessfulStatusCode = true
            };

            foreach (var item in _inventoryItems)
            {
                model.Items.Add(item.ToResponseModel());
            }

            return Delay()
                .ContinueWith(_ => model);
        }

        public Task<GetResponse> GetAsync(int id)
        {
            var model = new GetResponse();
            var item = _inventoryItems.FirstOrDefault(i => i.Id == id);

            if (item != null)
            {
                model = item.ToResponseModel();
            }

            return Delay()
                .ContinueWith(_ => model);
        }
    }
}