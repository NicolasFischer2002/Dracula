using IngredientInventory.Domain.Exceptions;
using SharedKernel.Formatters;

namespace IngredientInventory.Domain.ValueObjects
{
    public sealed record IngredientName
    {
        public string Name { get; }
        
        public IngredientName(string name)
        {
            Name = StringNormalizer.Normalize(name);
            Validate();
        }

        public void Validate()
        {
            const int MinimumLength = 1;
            const int MaximumLength = 100;

            IngredientInventoryException.ThrowIf(
                Name.Length < MinimumLength,
                Name,
                $"O nome do ingrediente é inválido. O mínimo de caracteres que ele pode possuir é {MinimumLength}."
            );

            IngredientInventoryException.ThrowIf(
               Name.Length > MaximumLength,
               Name,
               $"O nome do ingrediente é inválido. O máximo de caracteres que ele pode possuir é {MinimumLength}."
           );
        }

        public override string ToString() => Name;
    }
}