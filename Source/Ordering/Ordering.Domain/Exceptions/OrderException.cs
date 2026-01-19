using SharedKernel.Exceptions;

namespace Ordering.Domain.Exceptions
{
    public sealed class OrderException : CustomException<string>
    {
        public OrderException(string message) : base(message) { }
        public OrderException(string message, string invalidValue) : base(message, invalidValue) { }
        public OrderException(string message, Exception innerException) : base(message, innerException) { }
        public OrderException(string message, string invalidValue, Exception innerException)
            : base(message, invalidValue, innerException) { }

        public static void ThrowIf(bool condition, string invalidValue, string message)
        {
            if (condition)
                throw new OrderException(message, invalidValue);
        }

        public static void ThrowIf(bool condition, string message)
        {
            if (condition)
                throw new OrderException(message);
        }

        public static void ThrowIfNull<TValue>(TValue? value, string message) where TValue : class
        {
            if (value is null)
                throw new OrderException(message);
        }
    }
}