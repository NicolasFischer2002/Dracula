using Menu.Domain.Exceptions;

namespace Menu.Domain.ValueObjects
{
    public sealed record PreparationTimeInMinutes
    {
        public int Value { get; }

        public PreparationTimeInMinutes(int value)
        {
            Value = value;
            ValidateValue();
        }

        private void ValidateValue()
        {
            const int MinimumMinutes = 0;
            const int MinutesInDay = 1440;

            MenuException.ThrowIf(
                Value < MinimumMinutes,
                Value.ToString(),
                "O tempo de preparo do item do menu não pode ser negativo."
            );

            MenuException.ThrowIf(
                Value > MinutesInDay,
                Value.ToString(),
                $"O tempo de preparo não pode exceder {MinutesInDay} minutos (1 dia)."
            );
        }

        public override string ToString() => Value.ToString();
    }
}