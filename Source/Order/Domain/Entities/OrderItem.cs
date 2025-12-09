using Order.Domain.ValueObjects;

namespace Order.Domain.Entities
{
    public class OrderItem
    {     
        public Guid Id { get; init; }
        private OrderItemName Name { get; init; }
        private GrossOrderItemValue GrossOrderItemValue { get; init; }
        private OrderItemDiscount Discount { get; init; }
        private OrderItemDescription Description { get; init; }
        private DateTime DateItWasAdded { get; init; }

        public OrderItem(Guid id, OrderItemName name, GrossOrderItemValue grossOrderItemValue, 
            OrderItemDiscount discount, OrderItemDescription description, DateTime dateItWasAdded)
        {
            Id = id;
            Name = name;
            GrossOrderItemValue = grossOrderItemValue;
            Discount = discount;
            Description = description;
            DateItWasAdded = dateItWasAdded;
        }

        public decimal NetOrderItemValue => GrossOrderItemValue.Value - Discount.Value;
    }
}