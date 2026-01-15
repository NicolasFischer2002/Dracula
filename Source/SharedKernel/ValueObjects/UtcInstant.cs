using SharedKernel.Exceptions;

namespace SharedKernel.ValueObjects
{
    public sealed record UtcInstant
    {
        public DateTime Value { get; }

        public UtcInstant(DateTime value)
        {
            InvalidUtcInstantException.ThrowIf(
                value.Kind != DateTimeKind.Utc,
                value,
                "UtcInstant aceita apenas DateTime no fuso UTC (Kind.Utc)."
            );

            Value = value;
        }

        public static UtcInstant Now() => new(DateTime.UtcNow);
    }
}