using SharedKernel.Exceptions;

namespace SharedKernel.ValueObjects
{
    public sealed record UtcInstant
    {
        public DateTime Value { get; }

        public UtcInstant(DateTime value)
        {
            if (value.Kind != DateTimeKind.Utc)
                throw new InvalidUtcInstantException(value);

            Value = value;
        }

        public static UtcInstant Now() => new(DateTime.UtcNow);
    }
}