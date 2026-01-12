using Ordering.Domain.Exceptions;
using SharedKernel.Formatters;

namespace Ordering.Domain.ValueObjects
{
    public sealed record CookingInstructions
    {
        public string Note { get; }

        public CookingInstructions(string note)
        {
            Note = StringNormalizer.Normalize(note);
            ValidateNote();
        }

        private void ValidateNote()
        {
            const int MaximumNoteLength = 250;

            OrderException.ThrowIf(
                Note.Length > MaximumNoteLength,
                Note,
                $"Observações do item do pedido não podem exceder {MaximumNoteLength} caracteres."
            );
        }

        public override string ToString() => Note;
    }
}