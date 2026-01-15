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

            ValidateCurrency(Code);
        }

        private static void ValidateCurrency(string code)
        {
            InvalidCurrencyException.ThrowIf(
                string.IsNullOrWhiteSpace(code),
                "A moeda não pode ser nula."
            );

            var normalized = GetNormalizedCode(code);

            InvalidCurrencyException.ThrowIf(
                !ValidCurrencies.Contains(normalized),
                $"A moeda informada '{normalized}' não é válida."
            );
        }

        private static string GetNormalizedCode(string code) => code.Trim().ToUpperInvariant();

        public static Currency BRL => new("BRL");
        public static Currency USD => new("USD");
        public static Currency EUR => new("EUR");

        public override string ToString() => Code;
    }
}