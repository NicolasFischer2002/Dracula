using Menu.Domain.Exceptions;

namespace Menu.Domain.ValueObjects
{
    public sealed record Price
    {
        public decimal Amount { get; init; }

        public Price(decimal amount)
        {
            Amount = amount;
            ValidateAmount();
        }

        private void ValidateAmount()
        {
            const decimal MinAmount = 0m;
            const decimal MaxAmount = 999_999_999m;


            if (Amount < MinAmount)
            {
                throw new MenuException(
                    "O preço de um item do menu não pode ser negativo.",
                    Amount.ToString()
                );
            }

            if (Amount > MaxAmount) 
            {
                throw new MenuException(
                    $"O preço de um item do menu não pode ser maior que {MaxAmount}.",
                    Amount.ToString()
                );
            }
        }
    }
}