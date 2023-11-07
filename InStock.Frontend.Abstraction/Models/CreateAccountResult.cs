using InStock.Frontend.Abstraction.Models.Base;

namespace InStock.Frontend.Abstraction.Models
{
    public class CreateAccountResult : BaseResult
    {
        public bool AccountCreationSuccessful { get; set; }
    }
}