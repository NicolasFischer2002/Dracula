using Menu.Domain.Exceptions;

namespace Menu.Domain.ValueObjects
{
    public sealed record PreparationTimeInMinutes
    {
        public int Value { get; private set; }

        public PreparationTimeInMinutes(int value)
        {
            Value = value;

            ValidateValue();
        }

        private void ValidateValue()
        {
            const int Min = 0;
            const int MinutesInDay = 1440;

            if (Value < Min)
            {
                throw new MenuException(
                    "O tempo necessário para preparar um item do menu não pode ser negativo.",
                    Value.ToString()
                );
            }

            if (Value > MinutesInDay)
            {
                throw new MenuException(
                    $"O tempo necessário para preparar um item do menu não pode ser superior a " +
                    $"{MinutesInDay} minutos.",
                    Value.ToString()
                );
            }
        }  
    }
}