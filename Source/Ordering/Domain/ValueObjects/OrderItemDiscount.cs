using Ordering.Domain.Exceptions;

namespace Ordering.Domain.ValueObjects
{
    public sealed record OrderItemDiscount
    {
        public decimal Value { get; init; }

        public OrderItemDiscount(decimal value, decimal orderItemValue)
        {
            Value = value;

            ValidateDiscount(orderItemValue);
        }

        private void ValidateDiscount(decimal orderItemValue)
        {
            const int MinimumDiscount = 0;

            if (Value < MinimumDiscount)
            {
                throw new OrderException(
                    "O desconto aplicado ao item do pedido não pode ser negativo.", 
                    Value.ToString()
                );
            }

            if (Value > orderItemValue)
            {
                throw new OrderException(
                    "O desconto aplicado ao item do pedido não pode ser maior que o valor do item do pedido.", 
                    Value.ToString()
                );
            }
        }
    }
}