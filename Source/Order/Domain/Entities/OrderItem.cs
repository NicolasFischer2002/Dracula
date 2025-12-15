using Ordering.Domain.Enums;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Entities
{
    public class OrderItem
    {     
        public Guid Id { get; init; }
        public Guid WaiterId { get; init; }
        public OrderItemName Name { get; init; }
        public GrossOrderItemValue GrossOrderItemValue { get; init; }
        public OrderItemDiscount Discount { get; init; }
        public CookingInstructions CookingInstructions { get; init; }
        public DateTime DateItWasAdded { get; init; }
        public OrderItemStatus Status { get; private set; }
        public decimal NetOrderItemValue => GrossOrderItemValue.Value - Discount.Value;

        internal OrderItem(Guid id, Guid waiterId, OrderItemName name, GrossOrderItemValue grossOrderItemValue, 
            OrderItemDiscount discount, CookingInstructions cookingInstructions, DateTime dateItWasAdded,
            OrderItemStatus status)
        {
            Id = id;
            WaiterId = waiterId;
            Name = name;
            GrossOrderItemValue = grossOrderItemValue;
            Discount = discount;
            CookingInstructions = cookingInstructions;
            DateItWasAdded = dateItWasAdded;
            Status = status;
        }

        public void UpdateStatus(OrderItemStatus newStatus)
        {
            Status = newStatus;
        }
    }
}