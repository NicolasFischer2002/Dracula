using Ordering.Application.Commands.PlaceOrder;
using Ordering.Domain.Entities;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Factories.Implementations
{
    public sealed class OrderFactory : IOrderFactory
    {
        public async Task<Order> CreateAsync(CreateOrderRequest request, CancellationToken cancellationToken = default)
        {
            return new Order(
                Guid.NewGuid(),
                new Identifier("Berlim"),
                DateTime.UtcNow,
                new List<OrderItem>()
            );            
        }
    }
}