using Menu.Domain.Enums;
using Menu.Domain.Exceptions;

namespace Menu.Domain.ValueObjects
{
    public sealed record MenuItemQuantity
    {
        public decimal Amount { get; init; }
        public MenuItemUnit Unit { get; init; }

        public MenuItemQuantity(decimal amount, MenuItemUnit unit)
        {
            Amount = amount;
            Unit = unit;

            ValidadeAmount();
        }

        private void ValidadeAmount()
        {
            const int MinimumAmount = 0;

            if (Amount < MinimumAmount)
            {
                throw new MenuException(
                    $"A quantidade do item do menu deve ser maior que {MinimumAmount}.", 
                    Amount.ToString()
                );
            }
        }
    }
}