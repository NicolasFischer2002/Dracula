using Ordering.Domain.Entities;
using Ordering.Domain.Enums;
using Ordering.Domain.Exceptions;
using SharedKernel.ValueObjects;

namespace Ordering.Domain.EntityComposition
{
    public sealed class OrderItems
    {
        private List<OrderItem> _items { get; }
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
        private Currency Currency => _items.First().Currency;

        public OrderItems(IEnumerable<OrderItem> items)
        {
            ArgumentNullException.ThrowIfNull(items, nameof(items));

            _items = new List<OrderItem>(items);

            ValidateOrderItems(_items);
        }

        private void ValidateOrderItems(IEnumerable<OrderItem> items)
        {
            EnsureMinimumQuantityOfItems(items);
            EnsureThatAllItemsHaveTheSameCurrency(items);
        }

        private void EnsureMinimumQuantityOfItems(IEnumerable<OrderItem> items)
        {
            const int MinimumItemsQuantity = 1;

            if (items.Count() < MinimumItemsQuantity)
            {
                throw new OrderException(
                    $"A quantidade mínima de itens em um pedido deve ser de {MinimumItemsQuantity} item."
                );
            }
        }

        private void EnsureThatAllItemsHaveTheSameCurrency(IEnumerable<OrderItem> items)
        {
            if (items.GroupBy(i => i.Currency.Code).Count() > 1)
            {
                throw new OrderException("Todos os items do pedido devem possuir a mesma moeda.");
            }
        }

        public void Add(OrderItem orderItem)
        {
            _items.Add(orderItem);
        }

        public void Remove(Guid orderItemId)
        {
            if (!_items.Any(i => i.Id == orderItemId))
            {
                throw new OrderException(
                    $"O Id do item do pedido não foi encontrado.",
                    orderItemId.ToString()
                );
            }

            var projected = new List<OrderItem>(_items);
            projected.RemoveAll(item => item.Id == orderItemId);

            ValidateOrderItems(projected);

            _items.RemoveAll(item => item.Id == orderItemId);
        }

        public void UpdateItemStatus(Guid idOrderItem, OrderItemStatus newStatus)
        {
            _items
                .First(item => item.Id == idOrderItem)
                .UpdateStatus(newStatus);
        }

        public Money TotalValueOfAllItemsInTheOrder()
        {
            return new Money(_items.Sum(item => item.NetOrderItemValue().Amount), Currency);
        }
    }
}