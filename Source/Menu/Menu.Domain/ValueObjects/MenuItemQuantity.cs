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

            ValidateAmount();
        }

        private void ValidateAmount()
        {
            const int MinimumAmount = 0;

            MenuException.ThrowIf(
                Amount < MinimumAmount,
                Amount.ToString(),
                $"A quantidade do item do menu deve ser maior ou igual a {MinimumAmount}."
            );
        }

        public override string ToString() => $"{Amount} {Unit}";
    }
}