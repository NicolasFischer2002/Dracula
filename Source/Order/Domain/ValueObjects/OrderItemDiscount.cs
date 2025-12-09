using Order.Domain.Exceptions;

namespace Order.Domain.ValueObjects
{
    public sealed record OrderItemDiscount
    {
        public decimal Value { get; init; }
        private decimal OrderItemValue { get; init; }

        public OrderItemDiscount(decimal value, decimal orderItemValue)
        {
            Value = value;
            OrderItemValue = orderItemValue;

            ValidateDiscount();
        }

        private void ValidateDiscount()
        {
            const int MinimumDiscount = 0;

            if (Value < MinimumDiscount)
            {
                throw new OrderException(
                    "O desconto aplicado ao item do pedido não pode ser negativo.", 
                    Value.ToString()
                );
            }

            if (Value > OrderItemValue)
            {
                throw new OrderException(
                    "O desconto aplicado ao item do pedido não pode ser maior que o valor do item do pedido.", 
                    Value.ToString()
                );
            }
        }
    }
}