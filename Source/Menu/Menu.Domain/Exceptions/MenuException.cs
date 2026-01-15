using SharedKernel.Exceptions;

namespace Menu.Domain.Exceptions
{
    public sealed class MenuException : CustomException<string>
    {
        public MenuException(string message) : base(message) { }

        public MenuException(string message, string invalidValue)
            : base(message, invalidValue) { }

        public MenuException(string message, Exception innerException) : base(message, innerException) { }

        public MenuException(string message, string invalidValue, Exception innerException)
            : base(message, invalidValue, innerException) { }

        public static void ThrowIf(bool condition, string invalidValue, string message)
        {
            if (condition)
                throw new MenuException(message, invalidValue);
        }

        public static void ThrowIf(bool condition, string message)
        {
            if (condition)
                throw new MenuException(message);
        }

        public static void ThrowIfNull<TValue>(TValue? value, string message) where TValue : class
        {
            if (value is null)
                throw new MenuException(message);
        }
    }
}