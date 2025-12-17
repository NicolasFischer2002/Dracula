using SharedKernel.Exceptions;

namespace Ordering.Domain.Exceptions
{
    public class OrderException : CustomException<string>
    {
        public OrderException(string message, string invalidValue)
            : base(message, invalidValue)
        {
            
        }

        public OrderException(string message, string invalidValue, Exception innerException)
            : base(message, invalidValue, innerException)
        {
            
        }
    }
}