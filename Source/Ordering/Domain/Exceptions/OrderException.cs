using SharedKernel.Exceptions;

namespace Ordering.Domain.Exceptions
{
    public sealed class OrderException : CustomException<string>
    {
        public OrderException(string message) : base(message)
        {
        }

        public OrderException(string message, string invalidValue)
            : base(message, invalidValue)
        {
            
        }

        public OrderException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public OrderException(string message, string invalidValue, Exception innerException)
            : base(message, invalidValue, innerException)
        {
            
        }

        public static new OrderException Create(string message, string invalidValue)
        {
            return new OrderException(message, invalidValue);
        }
    }
}