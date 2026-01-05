using Ordering.Domain.Exceptions;
using SharedKernel.Formatters;

namespace Ordering.Domain.ValueObjects
{
    /// <summary>
    /// Represents the identifier of an order, acting as the order's table or ticket.
    /// </summary>
    public record Identifier
    {
        public string Id { get; }

        public Identifier(string id)
        {
            Id = StringNormalizer.Normalize(id);
            ValidateIdentifier();
        }

        private void ValidateIdentifier()
        {
            const int maxLength = 50;

            if (string.IsNullOrWhiteSpace(Id))
            {
                throw new OrderException(
                    $"O identificador do pedido não estar vazio.",
                    nameof(Id)
                );
            }

            if (Id.Length > maxLength)
            {
                throw new OrderException(
                    $"O identificador do pedido não pode possuir um comprimento maior que {maxLength} caracteres.",
                    nameof(Id)
                );
            }
        }
    }
}