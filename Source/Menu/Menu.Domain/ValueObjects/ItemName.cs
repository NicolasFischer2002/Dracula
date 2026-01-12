using Menu.Domain.Exceptions;
using SharedKernel.Formatters;

namespace Menu.Domain.ValueObjects
{
    public sealed record ItemName
    {
        public string Name { get; }

        public ItemName(string name)
        {
            MenuException.ThrowIfNull(name, nameof(name));
            Name = StringNormalizer.Normalize(name);
            ValidateName();
        }

        private void ValidateName()
        {
            const int MinimumLength = 1;
            const int MaximumLength = 150;

            MenuException.ThrowIf(
                Name.Length < MinimumLength,
                Name,
                $"O nome do item do menu deve ter pelo menos {MinimumLength} caractere(s)."
            );

            MenuException.ThrowIf(
                Name.Length > MaximumLength,
                Name,
                $"O nome do item do menu não pode exceder {MaximumLength} caractere(s)."
            );
        }

        public override string ToString() => Name;
    }
}