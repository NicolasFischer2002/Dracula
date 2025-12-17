using Ordering.Domain.Enums;
using Ordering.Domain.Exceptions;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; init; }
        public Identifier Identifier { get; init; }
        public OrderTimeline TimeLine { get; init; }
        private List<OrderItem> _items { get; init; }
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
        public OrderStatus Status { get; private set; }
        private decimal TotalValueOfAllItemsInTheOrder => _items.Sum(item => item.NetOrderItemValue);
        private decimal OrderDiscountAmount = 0;
        public decimal TotalOrderValue => TotalValueOfAllItemsInTheOrder - OrderDiscountAmount;

        internal Order(Guid id, Identifier identifier, OrderTimeline timeLine, 
            List<OrderItem> items, OrderStatus status)
        {
            Id = id;
            Identifier = identifier;
            TimeLine = timeLine;
            _items = items;
            Status = status;
        }

        public void AddItemToOrder(OrderItem item)
        {
            _items.Add(item);
            TimeLine.AddEvent(DateTime.UtcNow, ActionTimelineOfTheOrder.ItemAdded);
        }

        public void RemoveItemFromOrder(Guid orderItemId)
        {
            _items.RemoveAll(item => item.Id == orderItemId);
            TimeLine.AddEvent(DateTime.UtcNow, ActionTimelineOfTheOrder.ItemRemoved);
        }

        public void AddDiscountToOrder(decimal discount)
        {
            const int MinimumDiscount = 0;

            if (discount < MinimumDiscount)
            {
                throw new OrderException(
                    "O valor do desconto concedido ao pedido não pode ser negativo.", 
                    discount.ToString()
                );
            }

            if (discount > TotalValueOfAllItemsInTheOrder)
            {
                throw new OrderException(
                     "O valor do desconto concedido ao pedido não pode ser maior que o valor total do pedido.",
                     discount.ToString()
                );
            }

            OrderDiscountAmount = discount;
        }

        public void CloseOrder()
        {
            TimeLine.AddEvent(DateTime.UtcNow, ActionTimelineOfTheOrder.OrderCompleted);
            Status = OrderStatus.Completed;
        }

        public void UpdateOrderItemStatus(Guid idOrderItem, OrderItemStatus newStatus)
        {
            _items
                .Where(item => item.Id == idOrderItem)
                .First()
                .UpdateStatus(newStatus);
        }
    }
}