using InStock.Common.AccountService.Abstraction.Entities;

namespace InStock.Common.AccountService.Abstraction.TransferObjects.Addresses
{
    public class AddressesResponse
    {
        public IList<Address>? Addresses { get; set; }
    }
}