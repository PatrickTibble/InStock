using InStock.Common.InventoryService.Abstraction.TransferObjects.Get;

namespace InStock.Common.InventoryService.Abstraction.TransferObjects.GetAll
{
    public class GetAllResponse
    {
        public List<GetResponse> Items { get; set; }
        public bool IsSuccessfulStatusCode { get; set; }
    }
}