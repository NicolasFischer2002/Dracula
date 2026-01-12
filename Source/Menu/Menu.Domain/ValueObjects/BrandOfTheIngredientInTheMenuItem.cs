using Menu.Domain.Exceptions;
using SharedKernel.Formatters;

namespace Menu.Domain.ValueObjects
{
    public sealed record BrandOfTheIngredientInTheMenuItem
    {
        public string Brand { get; }

        public BrandOfTheIngredientInTheMenuItem(string brand)
        {
            Brand = StringNormalizer.Normalize(brand);

            ValidadeBrand();
        }

        private void ValidadeBrand() 
        { 
            const int MinimumLength = 0;
            const int MaximumLength = 100;

            MenuException.ThrowIf(
                Brand.Length < MinimumLength,
                Brand,
                $"A marca do item do menu não pode possuir menos de {MinimumLength} caracteres."
            );

            MenuException.ThrowIf(
                Brand.Length > MaximumLength,
                Brand,
                $"A marca do item do menu não pode possuir mais de {MaximumLength} caracteres."
            );
        }

        public static BrandOfTheIngredientInTheMenuItem Empty =>
            new(string.Empty);

        public override string ToString() => Brand;
    }
}