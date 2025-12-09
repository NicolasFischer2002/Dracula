using Order.Domain.Exceptions;

namespace Order.Domain.ValueObjects
{
    public sealed record OrderItemDescription
    {
        public string Description { get; init; }

        public OrderItemDescription(string description)
        {
            Description = description;

            ValidadeDescription();
        }

        private void ValidadeDescription()
        {
            const int MaximumDescriptionLength = 250;

            if (!string.IsNullOrWhiteSpace(Description))
            {
                if (Description.Length > MaximumDescriptionLength)
                {
                    throw new OrderException(
                        $"A descrição do item do pedido excedeu ao limite de {MaximumDescriptionLength} caracteres.",
                        Description
                    );
                }
            }
        }

        public override string ToString() => Description;
    }
}