using Ordering.Domain.Exceptions;

namespace Ordering.Domain.ValueObjects
{
    public sealed record CookingInstructions
    {
        public string Note { get; }

        public CookingInstructions(string note)
        {
            Note = note;
            ValidadeDescription();
        }

        private void ValidadeDescription()
        {
            const int MaximumDescriptionLength = 250;

            if (!string.IsNullOrWhiteSpace(Note))
            {
                if (Note.Length > MaximumDescriptionLength)
                {
                    throw new OrderException(
                        $"A observação do item do pedido excedeu ao limite de {MaximumDescriptionLength} caracteres.",
                        Note
                    );
                }
            }
        }

        public override string ToString() => Note;
    }
}