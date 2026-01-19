using Ordering.Domain.Exceptions;
using SharedKernel.Formatters;

namespace Ordering.Domain.ValueObjects
{
    public sealed record OrderItemName
    {
        public string Value { get; }

        public OrderItemName(string value)
        {
            Value = StringNormalizer.Normalize(value);
            ValidateName();
        }

        private void ValidateName()
        {
            OrderException.ThrowIf(
                string.IsNullOrWhiteSpace(Value),
                Value,
                "O nome do item do pedido não pode estar vazio."
            );

            const int MaximumLength = 150;

            OrderException.ThrowIf(
                Value.Length > MaximumLength,
                Value,
                $"O nome do item do pedido não pode exceder {MaximumLength} caracteres."
            );
        }

        public override string ToString() => Value;
    }
}