using Ordering.Domain.Exceptions;

namespace Ordering.Domain.ValueObjects
{
    public sealed record GrossOrderItemValue
    {
        public decimal Value { get; init; }

        public GrossOrderItemValue(decimal value)
        {
            Value = value;

            ValidateGrossValue();
        }

        private void ValidateGrossValue()
        {
            const int MinimumGrossValue = 0;
            
            if (Value < MinimumGrossValue)
            {
                throw new OrderException(
                    "O valor bruto do item do pedido não pode ser negativo.",
                    Value.ToString()
                );
            }
        }
    }
}