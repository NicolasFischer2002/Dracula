using Ordering.Application.Commands.PlaceOrder;
using Ordering.Domain.Entities;

namespace Ordering.Application.Factories
{
    public interface IOrderFactory
    {
        Task<Order> CreateAsync(CreateOrderRequest request, CancellationToken cancellationToken = default);
    }
}