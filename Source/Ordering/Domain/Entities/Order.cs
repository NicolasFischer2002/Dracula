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
            OrderException.ThrowIfNull(discount, "O dinheiro não pode estar nulo ao aplicar o desconto no pedido.");

            FailIfDifferentCurrency(discount);
            FailIfNegativeDiscount(discount);
            FailIfExceedsOrderTotal(discount);

            OrderDiscount = discount;
        }

        private void FailIfDifferentCurrency(Money discount)
        {
            OrderException.ThrowIf(
                !Money.SameCurrency(OrderDiscount, discount),
                discount.Currency.Code,
                "O desconto deve ter a mesma moeda do pedido."
            );
        }

        private static void FailIfNegativeDiscount(Money discount)
        {
            const decimal MinimumDiscount = 0;

            OrderException.ThrowIf(
                discount.Amount < MinimumDiscount,
                discount.Amount.ToString(),
                "O desconto não pode ser negativo."
            );
        }

        private void FailIfExceedsOrderTotal(Money discount)
        {
            var orderTotal = OrderItems.TotalValueOfAllItemsInTheOrder();

            OrderException.ThrowIf(
                discount.Amount > orderTotal.Amount,
                discount.Amount.ToString(),
                "O desconto não pode exceder o valor total do pedido."
            );
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