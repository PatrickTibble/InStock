using InStock.Common.Models.Base;

namespace InStock.Common.Models.Inventory.GetAll
{
    public class Response : IResponse
	{
        public bool IsSuccessfulStatusCode { get; set; }
        public IList<Get.Response>? Items { get; set; }
    }
}