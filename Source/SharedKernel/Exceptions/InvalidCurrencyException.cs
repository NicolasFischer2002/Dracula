namespace SharedKernel.Exceptions
{
    public sealed class InvalidCurrencyException : CustomException<string>
    {
        public InvalidCurrencyException(string message) : base(message) { }
        public InvalidCurrencyException(string message, string invalidValue) : base(message, invalidValue) { }
        public InvalidCurrencyException(string message, Exception innerException) : base(message, innerException) { }
        public InvalidCurrencyException(string message, string invalidValue, Exception innerException)
            : base(message, invalidValue, innerException) { }

        public static void ThrowIf(bool condition, string invalidValue, string message)
        {
            if (condition)
                throw new InvalidCurrencyException(message, invalidValue);
        }

        public static void ThrowIf(bool condition, string message)
        {
            if (condition)
                throw new InvalidCurrencyException(message);
        }

        public static void ThrowIfNull<TValue>(TValue? value, string message) where TValue : class
        {
            if (value is null)
                throw new InvalidCurrencyException(message);
        }
    }
}