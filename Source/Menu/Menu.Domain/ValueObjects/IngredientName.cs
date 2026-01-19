using Menu.Domain.Exceptions;
using SharedKernel.Formatters;

namespace Menu.Domain.ValueObjects
{
    public sealed record IngredientName
    {
        public string Name { get; }
        public string Abbreviation { get; }

        public IngredientName(string name, string abbreviation = "")
        {
            Name = StringNormalizer.Normalize(name);
            Abbreviation = StringNormalizer.Normalize(Name);
            Validate();
        }

        private void Validate()
        {
            ValidadeName();
            ValidateAbbreviation();
        }

        private void ValidadeName()
        {
            const int MinimumLength = 1;
            const int MaximumLength = 25;

            MenuException.ThrowIf(
                Name.Length < MinimumLength,
                Name,
                $"O nome do ingrediente do item do menu é inválido. " +
                $"O mínimo de caracteres que ele pode possuir é {MinimumLength}."
            );

            MenuException.ThrowIf(
                Name.Length > MaximumLength,
                Name,
                $"O nome do ingrediente do item do menu é inválido. " +
                $"O máximo de caracteres que ele pode possuir é {MaximumLength}."
            );
        }

        private void ValidateAbbreviation()
        {
            const int MaximumLength = 10;
            MenuException.ThrowIf(
                Abbreviation.Length > MaximumLength,
                Abbreviation,
                $"A abreviação do nome do item do menu é inválida. " +
                $"O máximo de caracteres que ela pode possuir é {MaximumLength}."
            );
        }

        public override string ToString() => Name;
    }
}