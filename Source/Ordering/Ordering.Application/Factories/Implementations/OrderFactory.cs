using Ordering.Application.Commands.PlaceOrder;
using Ordering.Domain. Entities;
using Ordering.Domain.EntityComposition;
using Ordering.Domain.Enums;
using Ordering.Domain.ValueObjects;
using SharedKernel.ValueObjects;

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
                requestForOrderItems.Select(itemRequest => new OrderItem(
                    Guid.NewGuid(),
                    itemRequest.WaiterId,
                    new OrderItemName(itemRequest.NameItem),
                    new GrossOrderItemValue(itemRequest.GrossValue),
                    new OrderItemDiscount(itemRequest.GrossValue, itemRequest.Discount),
                    new CookingInstructions(itemRequest.CookingInstructions),
                    new OrderItemTimeline(new UtcInstant(DateTime.UtcNow)),
                    OrderItemStatus.Waiting
                )).ToList(),
                new OrderTimeline(new UtcInstant(DateTime.UtcNow)),
                OrderStatus.Open,
                Money.Zero(orderRequest.Currency)
            );            
        }
    }
}