namespace InStock.Common.Models.Base
{
    public abstract class BaseRequest : IRequest
	{
        public bool IsValid => Validate();

        protected abstract bool Validate();
    }
}