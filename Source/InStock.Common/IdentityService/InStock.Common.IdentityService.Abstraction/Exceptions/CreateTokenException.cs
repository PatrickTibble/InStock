using System.Runtime.Serialization;

namespace InStock.Common.IdentityService.Abstraction.Exceptions
{
    public class CreateTokenException : Exception
    {
        public CreateTokenException()
        {
        }

        public CreateTokenException(string? message) : base(message)
        {
        }

        public CreateTokenException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CreateTokenException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
