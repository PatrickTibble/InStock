using InStock.Common.Models.Base;

namespace InStock.Common.Models.Inventory.Get
{
    public class Response : IResponse
	{
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public bool IsSuccessfulStatusCode { get; set; }
    }
}