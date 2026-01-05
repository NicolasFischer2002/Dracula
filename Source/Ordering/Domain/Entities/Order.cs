using Ordering.Domain.EntityComposition;
using Ordering.Domain.Enums;
using Ordering.Domain.Exceptions;
using Ordering.Domain.ValueObjects;
using SharedKernel.ValueObjects;

namespace Ordering.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; }
        public Identifier Identifier { get; }
        public OrderItems OrderItems { get; }
        public OrderTimeline TimeLine { get; }
        public OrderStatus Status { get; private set; }
        private Money OrderDiscount { get; set; }

        internal Order(Guid id, Identifier identifier, IEnumerable<OrderItem> items, 
            OrderTimeline timeLine, OrderStatus status, Money orderDiscount)
        {
            Id = id;
            Identifier = identifier;
            OrderItems = new OrderItems(items);
            TimeLine = timeLine;
            Status = status;
            OrderDiscount = orderDiscount;
        }

        public void AddItemToOrder(OrderItem item)
        {
            OrderItems.Add(item);
            TimeLine.AddEvent(new UtcInstant(DateTime.UtcNow), ActionTimelineOfTheOrder.ItemAdded);
        }

        public void RemoveItemFromOrder(Guid orderItemId)
        {
            OrderItems.Remove(orderItemId);
            TimeLine.AddEvent(new UtcInstant(DateTime.UtcNow), ActionTimelineOfTheOrder.ItemRemoved);
        }

        public void AddDiscountToOrder(Money discount)
        {
            const int MinimumDiscount = 0;

            if (!Money.SameCurrency(OrderDiscount, discount))
            {
                throw new OrderException(
                    "A moeda do desconto concedido ao pedido deve ser a mesma do pedido.", 
                    discount.Currency.Code
                );
            }

            if (discount.Amount < MinimumDiscount)
            {
                throw new OrderException(
                    "O valor do desconto concedido ao pedido não pode ser negativo.", 
                    discount.ToString()
                );
            }

            if (discount.Amount > OrderItems.TotalValueOfAllItemsInTheOrder().Amount)
            {
                throw new OrderException(
                     "O valor do desconto concedido ao pedido não pode ser maior que o valor total do pedido.",
                     discount.Amount.ToString()
                );
            }

            OrderDiscount = discount;
        }

        public void CloseOrder()
        {
            TimeLine.AddEvent(new UtcInstant(DateTime.UtcNow), ActionTimelineOfTheOrder.OrderCompleted);
            Status = OrderStatus.Completed;
        }

        public void UpdateOrderItemStatus(Guid idOrderItem, OrderItemStatus newStatus)
        {
            OrderItems.UpdateItemStatus(idOrderItem, newStatus);
        }

        public Money TotalOrderValue()
        {
            return OrderItems.TotalValueOfAllItemsInTheOrder().Subtract(OrderDiscount);
        }
    }
}