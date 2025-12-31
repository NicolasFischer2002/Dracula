using SharedKernel.Exceptions;
using SharedKernel.Formatters;

namespace SharedKernel.ValueObjects
{
    public sealed record Currency
    {
        private static readonly HashSet<string> ValidCurrencies =
            new(StringComparer.OrdinalIgnoreCase) { "BRL", "USD", "EUR" };

        public string Code { get; }

        public Currency(string code)
        {
            Code = StringNormalizer.Normalize(code);

            ValidateCurrency();
        }

        private void ValidateCurrency()
        {
            if (string.IsNullOrWhiteSpace(Code))
                throw new InvalidCurrencyException(Code);

            var normalized = Code.Trim().ToUpperInvariant();

            if (!ValidCurrencies.Contains(normalized))
                throw new InvalidCurrencyException(normalized);
        }

        public static Currency BRL => new("BRL");
        public static Currency USD => new("USD");
        public static Currency EUR => new("EUR");

        public override string ToString() => Code;
    }
}