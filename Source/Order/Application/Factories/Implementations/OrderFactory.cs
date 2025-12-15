using Ordering.Application.Commands.PlaceOrder;
using Ordering.Domain.Entities;
using Ordering.Domain.Enums;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Factories.Implementations
{
    public sealed class OrderFactory : IOrderFactory
    {
        public async Task<Order> CreateAsync(
            CreateOrderRequest orderRequest,
            List<CreateOrderItemRequest> requestForOrderItems,
            CancellationToken cancellationToken = default)
        {
            return new Order(
                Guid.NewGuid(),
                new Identifier(orderRequest.Identifier),
                new OrderTimeline(DateTime.UtcNow),
                requestForOrderItems.Select(itemRequest => new OrderItem(
                    Guid.NewGuid(),
                    itemRequest.WaiterId,
                    new OrderItemName(itemRequest.NameItem),
                    new GrossOrderItemValue(itemRequest.GrossValue),
                    new OrderItemDiscount(itemRequest.GrossValue, itemRequest.Discount),
                    new CookingInstructions(itemRequest.CookingInstructions),
                    new OrderItemTimeline(DateTime.UtcNow),
                    OrderItemStatus.Waiting
                )).ToList(),
                OrderStatus.Open
            );            
        }
    }
}