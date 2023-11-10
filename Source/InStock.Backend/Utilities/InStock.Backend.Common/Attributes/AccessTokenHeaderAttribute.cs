using Refit;

namespace InStock.Backend.Common.Attributes
{
    public class AccessTokenHeaderAttribute : HeaderAttribute
    {
        public AccessTokenHeaderAttribute() : base("accessToken")
        {
        }
    }
}