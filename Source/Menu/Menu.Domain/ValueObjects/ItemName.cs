using Menu.Domain.Exceptions;
using SharedKernel.Formatters;

namespace Menu.Domain.ValueObjects
{
    public sealed record ItemName
    {
        public string Name { get; private set; }

        public ItemName(string name)
        {
            Name = StringNormalizer.Normalize(name);
            ValidateName();
        }

        private void ValidateName()
        {
            const int minLength = 1;
            const int maxLength = 150;

            if (Name.Length < minLength)
            {
                throw new MenuException(
                    $"O nome de um item do menu não pode possuir menos de {minLength} caracteres.",
                    Name
                );
            }

            if (Name.Length > maxLength)
            {
                throw new MenuException(
                    $"O nome de um item do menu não pode possuir mais de {maxLength} caracteres.",
                    Name
                );
            }
        }

        public override string ToString() => Name;
    }
}