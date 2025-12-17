using Ordering.Domain.Exceptions;

namespace Ordering.Domain.ValueObjects
{
    public sealed record OrderItemName
    {
        public string Value { get; init; }

        public OrderItemName(string value)
        {
            Value = value.Trim();

            ValidateName();
        }

        private void ValidateName()
        {
            if (string.IsNullOrWhiteSpace(Value))
            {
                throw new OrderException(
                    "O nome do item do pedido não estar vazio.",
                    string.Empty
                );
            }

            const int maxLength = 150;

            if (Value.Length > maxLength)
            {
                throw new OrderException(
                    $"O nome do item do pedido excede a quantidade máxima de {maxLength} caracteres.",
                    Value
                );
            }
        }

        public override string ToString() => Value;
    }
}