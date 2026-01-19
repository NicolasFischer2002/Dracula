using Ordering.Domain.Entities;
using Ordering.Domain.Enums;
using Ordering.Domain.Exceptions;
using SharedKernel.ValueObjects;

namespace Ordering.Domain.EntityComposition
{
    public sealed class OrderItems
    {
        private readonly List<OrderItem> _items;
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

        /// <summary>
        /// Gets the currency from the first item. Assumes all items have the same currency
        /// due to validation invariants.
        /// </summary>
        private Currency Currency => _items.First().Currency;

        public OrderItems(IEnumerable<OrderItem> items)
        {
            OrderException.ThrowIfNull(items, "Os itens do pedido não podem ser nulos.");
            _items = [.. items];
            EnsureValidState(_items);
        }

        public void Add(OrderItem orderItem)
        {
            OrderException.ThrowIfNull(orderItem, "O item que será adicionado ao pedido não pode ser nulo.");
            _items.Add(orderItem);
        }

        public void Remove(Guid orderItemId)
        {
            FailIfItemNotFound(orderItemId);
            var projectedItems = new List<OrderItem>(_items.Where(item => item.Id != orderItemId));

            EnsureValidState(projectedItems);
            _items.RemoveAll(item => item.Id == orderItemId);
        }

        public void UpdateItemStatus(Guid orderItemId, OrderItemStatus newStatus)
        {
            var item = _items.FirstOrDefault(item => item.Id == orderItemId);

            OrderException.ThrowIfNull(
                item,
                "Item do pedido não encontrado para atualização de status."
            );

            item!.UpdateStatus(newStatus);
        }

        /// <summary>
        /// Calculates the total gross value of all order items in the order currency.
        /// </summary>
        public Money TotalValueOfAllItemsInTheOrder()
        {
            var totalAmount = _items.Sum(item => item.NetOrderItemValue().Amount);
            return new Money(totalAmount, Currency);
        }

        private static void EnsureValidState(IReadOnlyCollection<OrderItem> items)
        {
            OrderException.ThrowIfNull(items, "Os items do pedido não podem ser nulos.");

            EnsureMinimumQuantityOfItems(items);
            EnsureAllItemsHaveSameCurrency(items);
        }

        private static void EnsureMinimumQuantityOfItems(IReadOnlyCollection<OrderItem> items)
        {
            const int MinimumItemsQuantity = 1;

            OrderException.ThrowIf(
                items.Count < MinimumItemsQuantity,
                $"Um pedido deve conter pelo menos {MinimumItemsQuantity} item(ns)."
            );
        }

        private static void EnsureAllItemsHaveSameCurrency(IReadOnlyCollection<OrderItem> items)
        {
            var currencyGroups = items.GroupBy(i => i.Currency.Code).ToList();

            OrderException.ThrowIf(
                currencyGroups.Count > 1,
                "Todos os itens do pedido devem possuir a mesma moeda."
            );
        }

        private void FailIfItemNotFound(Guid orderItemId)
        {
            OrderException.ThrowIf(
                !_items.Any(i => i.Id == orderItemId),
                orderItemId.ToString(),
                "Item do pedido não encontrado."
            );
        }
    }
}