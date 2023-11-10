namespace InStock.Frontend.Abstraction.Models.Base
{
    public abstract class BaseResult
    {
        protected BaseResult() { }

        public string? ErrorMessage { get; set; }
    }
}