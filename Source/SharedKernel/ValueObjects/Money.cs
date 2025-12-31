using SharedKernel.Exceptions;

namespace SharedKernel.ValueObjects
{
    public sealed record Money
    {
        public decimal Amount { get; }
        public Currency Currency { get; }

        public Money(decimal amount, Currency currency)
        {
            Currency = currency ?? throw new InvalidMoneyException(
                "A moeda não pode ser nula.",
                amount
            );

            Amount = Normalize(amount);

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
            if (Amount < 0)
            {
                throw new InvalidMoneyException(
                    "O valor monetário não pode ser negativo.",
                    Amount
                );
            }
        }

        public static Money Zero(Currency currency) =>
            new(0m, currency);

        public override string ToString() =>
            $"{Currency} {Amount:N2}";

        public Money Add(Money other)
        {
            EnsureSameCurrency(other);
            return new Money(Amount + other.Amount, Currency);
        }

        public Money Subtract(Money other)
        {
            EnsureSameCurrency(other);
            return new Money(Amount - other.Amount, Currency);
        }

        private void EnsureSameCurrency(Money other)
        {
            if (Currency != other.Currency)
                throw new InvalidMoneyException(
                    "Não é possível operar valores com moedas diferentes.",
                    Amount
                );
        }
    }
}