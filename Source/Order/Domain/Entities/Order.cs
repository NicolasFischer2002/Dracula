using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; init; }
        public Identifier Identifier { get; init; }
        public DateTime CreatedAt { get; init; }
        private List<OrderItem> _items { get; init; }
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

        internal Order(Guid id, Identifier identifier, DateTime createdAt, List<OrderItem> items)
        {
            Id = id;
            Identifier = identifier;
            CreatedAt = createdAt;
            _items = items;
        }
    }
}