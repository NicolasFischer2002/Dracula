using Ordering.Domain.Exceptions;
using SharedKernel.ValueObjects;

namespace Ordering.Domain.ValueObjects
{
    public sealed record GrossOrderItemValue
    {
        public Money Value { get; }

        public GrossOrderItemValue(Money value)
        {
            Value = value;

            ValidateGrossValue();
        }

        private void ValidateGrossValue()
        {
            const int MinimumGrossValue = 0;
            
            if (Value.Amount < MinimumGrossValue)
            {
                throw new OrderException(
                    "O valor bruto do item do pedido não pode ser negativo.",
                    Value.ToString()
                );
            }
        }
    }
}