namespace Order.Domain.Entities
{
    // Berlim => Antes de continuar, criar uma Factory para Order.
    public class Order
    {
        public Guid Id { get; init; }
        public DateTime CreatedAt { get; init; }
        private List<OrderItem> _items { get; init; }
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

        public Order(DateTime createdAt, List<OrderItem> items)
        {
            Id = Guid.NewGuid();
            CreatedAt = createdAt;
            _items = items;
        }
    }
}