using SharedKernel.Exceptions;

namespace SharedKernel.ValueObjects
{
    public sealed record Money
    {
        public decimal Amount { get; }
        public Currency Currency { get; }

        public Money(decimal amount, Currency currency)
        {
            InvalidMoneyException.ThrowIfNull(
                currency,
                "A moeda não pode ser nula."
            );

            Amount = Normalize(amount);
            Currency = currency;

            Validate();
        }

        /// <summary>
        /// Normalizes a monetary amount by rounding it to a fixed number of decimal places.
        /// </summary>
        /// <param name="amount">
        /// The raw monetary value to be normalized.
        /// </param>
        /// <returns>
        /// A normalized monetary value rounded to two decimal places.
        /// </returns>
        /// <remarks>
        /// This normalization ensures consistency across the domain by enforcing a single
        /// rounding rule for all monetary values.
        /// 
        /// The rounding strategy used is <see cref="MidpointRounding.AwayFromZero"/>, which is
        /// appropriate for financial calculations, as it avoids the unexpected results of
        /// banker's rounding (<see cref="MidpointRounding.ToEven"/>).
        /// 
        /// By normalizing monetary values at the point of creation, the domain guarantees
        /// that all monetary operations (addition, subtraction, persistence, comparison)
        /// operate on consistent and predictable values.
        /// </remarks>
        private static decimal Normalize(decimal amount)
            => decimal.Round(amount, 2, MidpointRounding.AwayFromZero);

        private void Validate()
        {
            InvalidMoneyException.ThrowIf(
                Amount < 0,
                Amount,
                "O valor monetário não pode ser negativo."
            );
        }

        public static Money Zero(Currency currency) =>
            new(0m, currency);

        public override string ToString() =>
            $"{Currency} {Amount:N2}";

        public Money Add(Money other)
        {
            FailIfTheMoneyIsNull(other);

            EnsureSameCurrency(other);
            return new Money(Amount + other.Amount, Currency);
        }

        public Money Subtract(Money other)
        {
            FailIfTheMoneyIsNull(other);

            EnsureSameCurrency(other);
            return new Money(Amount - other.Amount, Currency);
        }

        private void EnsureSameCurrency(Money other)
        {
            FailIfTheMoneyIsNull(other);

            InvalidMoneyException.ThrowIf(
                !SameCurrency(this, other),
                Amount,
                "Não é possível operar valores com moedas diferentes."
            );
        }

        private static void FailIfTheMoneyIsNull(Money money)
        {
            InvalidMoneyException.ThrowIfNull(
                money,
                "O valor monetário fornecido não pode ser nulo."
            );
        }

        public static bool SameCurrency(Money money_1, Money money_2) => money_1.Currency == money_2.Currency;
    }
}