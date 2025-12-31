using Menu.Domain.Exceptions;
using SharedKernel.Formatters;

namespace Menu.Domain.ValueObjects
{
    public sealed record BrandOfTheIngredientInTheMenuItem
    {
        public string Brand { get; init; }

        public BrandOfTheIngredientInTheMenuItem(string brand)
        {
            Brand = StringNormalizer.Normalize(brand);

            ValidadeBrand();
        }

        private void ValidadeBrand() 
        { 
            const int MinimumLength = 0;
            const int MaximumLength = 100;

            if (Brand.Length < MinimumLength)
            {
                throw new MenuException(
                    $"A marca do item do menu não pode possuir menos de {MinimumLength} caracteres.", 
                    Brand
                );
            }

            if (Brand.Length > MaximumLength)
            {
                throw new MenuException(
                    $"A marca do item do menu não pode possuir mais de {MaximumLength} caracteres.",
                    Brand
                );
            }
        }

        public static BrandOfTheIngredientInTheMenuItem Empty =>
            new(string.Empty);

        public override string ToString() => Brand;
    }
}