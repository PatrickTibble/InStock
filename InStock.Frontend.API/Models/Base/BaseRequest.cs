namespace InStock.Frontend.API.Models.Base
{
    public abstract class BaseRequest : IRequest
	{
        public bool IsValid => Validate();

        protected abstract bool Validate();
    }
}