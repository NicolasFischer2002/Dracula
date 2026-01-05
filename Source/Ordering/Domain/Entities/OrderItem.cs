using Ordering.Domain.Enums;
using Ordering.Domain.Exceptions;
using Ordering.Domain.ValueObjects;
using SharedKernel.ValueObjects;

namespace Ordering.Domain.Entities
{
    public class OrderItem
    {     
        public Guid Id { get; }
        public Guid WaiterId { get; }
        public OrderItemName Name { get; }
        private GrossOrderItemValue GrossOrderItemValue { get; }
        private OrderItemDiscount Discount { get; }
        public CookingInstructions CookingInstructions { get; }
        public OrderItemTimeline OrderItemTimeline { get; }
        public OrderItemStatus Status { get; private set; }
        public Currency Currency { get; } 

        internal OrderItem(Guid id, Guid waiterId, OrderItemName name, GrossOrderItemValue grossOrderItemValue, 
            OrderItemDiscount discount, CookingInstructions cookingInstructions, 
            OrderItemTimeline orderItemTimeline, OrderItemStatus status)
        {
            Id = id;
            WaiterId = waiterId;
            Name = name;
            GrossOrderItemValue = grossOrderItemValue;
            Discount = discount;
            CookingInstructions = cookingInstructions;
            OrderItemTimeline = orderItemTimeline;
            Status = status;

            Currency = grossOrderItemValue.Value.Currency;

            ValidateOrderItem();
        }

        private void ValidateOrderItem()
        {
            if (!Money.SameCurrency(GrossOrderItemValue.Value, Discount.Value))
            {
                throw new OrderException(
                    "O valor bruto do item do pedido não pode ser de uma moeda diferente do valor desconto " +
                    "concedido pedido."
                );
            }
        }

        public void UpdateStatus(OrderItemStatus newStatus)
        {
            Status = newStatus;
        }

        public Money NetOrderItemValue()
        {
            return GrossOrderItemValue.Value.Subtract(Discount.Value);
        }
    }
}