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
            Value = value;
            OrderItemValue = orderItemValue;

            ValidateDiscount();
        }

        private void ValidateDiscount()
        {
            const int MinimumDiscount = 0;

            if (Value.Amount < MinimumDiscount)
            {
                throw new OrderException(
                    "O desconto aplicado ao item do pedido não pode ser negativo.", 
                    Value.ToString()
                );
            }

            if (Value.Amount > OrderItemValue.Amount)
            {
                throw new OrderException(
                    "O desconto aplicado ao item do pedido não pode ser maior que o valor do item do pedido.", 
                    Value.ToString()
                );
            }
        }
    }
}