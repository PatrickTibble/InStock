using InStock.Common.InventoryService.Abstraction.TransferObjects.Get;
using InStock.Common.InventoryService.Abstraction.TransferObjects.GetAll;
using InStock.Common.InventoryService.Abstraction.TransferObjects.Delete;
using InStock.Common.InventoryService.Abstraction.TransferObjects.Insert;
using InStock.Common.InventoryService.Abstraction.Services;
using InStock.Common.Models.Base;

namespace InStock.Frontend.API.Mocks
{
    public class MockInventoryService : BaseMockService, IInventoryService
    {
        private IList<MockInventoryItem> _inventoryItems = new List<MockInventoryItem>()
        {
            new MockInventoryItem(1, "Test inventory item 2", "Test inventory item description. Lorem ipsum"),
            new MockInventoryItem(2, "Test inventory item 3", "Test inventory item description. Lorem ipsum"),
            new MockInventoryItem(3, "Test inventory item 4", "Test inventory item description. Lorem ipsum"),
            new MockInventoryItem(4, "Test inventory item 5", "Test inventory item description. Lorem ipsum"),
            new MockInventoryItem(5, "Test inventory item 6", "Test inventory item description. Lorem ipsum")
        };

        public Task<Result<InsertResponse>> AddOrUpdateAsync(InsertRequest request)
        {
            var response = default(Result<InsertResponse>);
            if (request.Id == 0)
            {
                _inventoryItems.Add(new MockInventoryItem(_inventoryItems.Count + 1, request.Name, request.Description));
            }
            else
            {
                var item = _inventoryItems.FirstOrDefault(i => i.Id == request.Id);

                if (item != null)
                {
                    item.Name = request.Name;
                    item.Description = request.Description;
                }
                else
                {
                    response = new Result<InsertResponse>(400, "Item not found");
                }
            }
            return Delay()
                .ContinueWith(_ => response ?? new Result<InsertResponse>(new InsertResponse
                {
                    Result = true
                }));
        }

        public Task<Result<DeleteResponse>> DeleteAsync(int id)
        {
            var response = default(Result<DeleteResponse>);
            var item = _inventoryItems.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                _inventoryItems.Remove(item);
                response = new Result<DeleteResponse>(new DeleteResponse
                {
                    Result = true
                });
            }
            else
            {
                response = new Result<DeleteResponse>(400, "Item not found");
            }
            return Delay()
                .ContinueWith(_ => response);
        }

        public Task<Result<GetAllResponse>> GetAllAsync()
        {
            var model = new Result<GetAllResponse>(new GetAllResponse
            {
                Items = new List<GetResponse>(),
                IsSuccessfulStatusCode = true
            });

            foreach (var item in _inventoryItems)
            {
                model.Data!.Items.Add(item.ToResponseModel());
            }

            return Delay()
                .ContinueWith(_ => model);
        }

        public Task<Result<GetResponse>> GetAsync(int id)
        {
            var model = new Result<GetResponse>();
            var item = _inventoryItems.FirstOrDefault(i => i.Id == id);

            if (item != null)
            {
                model.Data = item.ToResponseModel();
                model.StatusCode = 200;
            }
            else
            {
                model.StatusCode = 400;
                model.Error = "Item not found";
            }

            return Delay()
                .ContinueWith(_ => model);
        }
    }
}