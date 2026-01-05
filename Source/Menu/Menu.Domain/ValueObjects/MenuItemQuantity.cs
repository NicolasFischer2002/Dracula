using Menu.Domain.Enums;
using Menu.Domain.Exceptions;

namespace Menu.Domain.ValueObjects
{
    public sealed record MenuItemQuantity
    {
        public int Amount { get; }
        public MenuItemUnit Unit { get; }

        public MenuItemQuantity(int amount, MenuItemUnit unit)
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