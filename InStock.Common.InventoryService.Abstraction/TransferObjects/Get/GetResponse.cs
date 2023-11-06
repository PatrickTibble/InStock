namespace InStock.Common.InventoryService.Abstraction.TransferObjects.Get
{
    public class GetResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsSuccessfulStatusCode { get; set; }
    }
}