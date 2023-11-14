using InStock.Frontend.Abstraction.Models.Base;

namespace InStock.Frontend.Abstraction.Models
{
    public class BooleanResult : BaseResult
    {
        public bool Result { get; set; } = false;
    }
}