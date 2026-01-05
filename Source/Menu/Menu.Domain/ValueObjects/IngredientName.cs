using Menu.Domain.Exceptions;
using SharedKernel.Formatters;

namespace Menu.Domain.ValueObjects
{
    public sealed record IngredientName
    {
        public string Name { get; }

        public IngredientName(string name)
        {
            Name = StringNormalizer.Normalize(name);

            ValidadeName();
        }

        private void ValidadeName()
        {
            const int MinimumLength = 1;
            const int MaximumLength = 100;

            if (Name.Length < MinimumLength)
            {
                throw new MenuException(
                    $"O nome do item do menu é inválido. O mínimo de caracteres que ele pode " +
                    $"possuir é {MinimumLength}.",
                    Name
                );
            }

            if (Name.Length > MaximumLength)
            {
                throw new MenuException(
                    $"O nome do item do menu é inválido. O máximo de caracteres que ele pode " +
                    $"possuir é {MaximumLength}.",
                    Name
                );
            }
        }

        public override string ToString() => Name;
    }
}