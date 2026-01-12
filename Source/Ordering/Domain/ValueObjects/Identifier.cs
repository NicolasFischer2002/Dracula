using Ordering.Domain.Exceptions;
using SharedKernel.Formatters;

namespace Ordering.Domain.ValueObjects
{
    /// <summary>
    /// Represents the identifier of an order, acting as the order's table or ticket.
    /// </summary>
    public sealed record Identifier
    {
        public string Id { get; }

        public Identifier(string id)
        {
            Id = StringNormalizer.Normalize(id);
            ValidateIdentifier();
        }

        private void ValidateIdentifier()
        {
            const int MaximumLength = 50;

            OrderException.ThrowIf(
                string.IsNullOrWhiteSpace(Id),
                Id,
                "O identificador do pedido não pode estar vazio."
            );

            OrderException.ThrowIf(
                Id.Length > MaximumLength,
                Id,
                $"O identificador do pedido não pode exceder {MaximumLength} caracteres."
            );
        }

        public override string ToString() => Id;
    }
}