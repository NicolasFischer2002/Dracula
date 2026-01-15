namespace SharedKernel.Exceptions
{
    public sealed class InvalidUtcInstantException : CustomException<DateTime>
    {
        public InvalidUtcInstantException(string message) : base(message) { }

        public InvalidUtcInstantException(string message, DateTime invalidValue)
            : base(message, invalidValue) { }

        public InvalidUtcInstantException(string message, Exception innerException)
            : base(message, innerException) { }

        public InvalidUtcInstantException(string message, DateTime invalidValue, Exception innerException)
            : base(message, invalidValue, innerException) { }

        public InvalidUtcInstantException(DateTime providedValue)
            : base($"UtcInstant aceita apenas DateTime UTC. Fornecido: {providedValue.Kind}.", providedValue)
        {
        }

        public static void ThrowIf(bool condition, DateTime invalidValue, string message)
        {
            if (condition)
                throw new InvalidUtcInstantException(message, invalidValue);
        }

        public static void ThrowIf(bool condition, string message)
        {
            if (condition)
                throw new InvalidUtcInstantException(message);
        }

        public static void ThrowIfNull<TValue>(TValue? value, string message)
            where TValue : class
        {
            if (value is null)
                throw new InvalidUtcInstantException(message);
        }

        public static void ThrowIfNotUtc(DateTime dateTime, string paramName)
        {
            if (dateTime.Kind != DateTimeKind.Utc)
                throw new InvalidUtcInstantException(dateTime);
        }

        public static void ThrowIfInvalidUtc(DateTime dateTime, string paramName)
        {
            if (dateTime.Kind != DateTimeKind.Utc)
                throw new InvalidUtcInstantException($"Parâmetro '{paramName}' deve ser UTC.", dateTime);
        }
    }
}