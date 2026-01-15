using Ordering.Domain.Exceptions;
using SharedKernel.ValueObjects;

namespace Ordering.Domain.ValueObjects
{
    public sealed record GrossOrderItemValue
    {
        public Money Value { get; }

        public GrossOrderItemValue(Money value)
        {
            OrderException.ThrowIfNull(value, "O valor bruto do item do pedido não pode ser nulo.");
            Value = value;
            ValidateGrossValue();
        }

        private void ValidateGrossValue()
        {
            const decimal MinimumGrossValue = 0m;

            OrderException.ThrowIf(
                Value.Amount < MinimumGrossValue,
                Value.Amount.ToString(),
                "O valor bruto do item do pedido não pode ser negativo."
            );
        }

        public override string ToString() => Value.ToString();
    }
}