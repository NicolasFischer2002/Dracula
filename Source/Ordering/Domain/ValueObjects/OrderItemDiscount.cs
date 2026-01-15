using Ordering.Domain.Exceptions;
using SharedKernel.ValueObjects;

namespace Ordering.Domain.ValueObjects
{
    public sealed record OrderItemDiscount
    {
        public Money Value { get; }
        private readonly Money OrderItemValue;

        public OrderItemDiscount(Money value, Money orderItemValue)
        {
            OrderException.ThrowIfNull(value, "O valor do desconto do item do pedido não pode ser nulo.");
            OrderException.ThrowIfNull(orderItemValue, "O valor do item do pedido não pode ser nulo.");

            Value = value;
            OrderItemValue = orderItemValue;

            ValidateDiscount();
        }

        private void ValidateDiscount()
        {
            const decimal MinimumDiscount = 0m;

            OrderException.ThrowIf(
                Value.Amount < MinimumDiscount,
                Value.Amount.ToString(),
                "O desconto aplicado ao item do pedido não pode ser negativo."
            );

            OrderException.ThrowIf(
                Value.Amount > OrderItemValue.Amount,
                Value.Amount.ToString(),
                "O desconto não pode exceder o valor do item do pedido."
            );
        }

        public override string ToString() => Value.ToString();
    }
}