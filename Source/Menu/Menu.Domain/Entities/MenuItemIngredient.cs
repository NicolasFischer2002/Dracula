using Menu.Domain.Exceptions;
using Menu.Domain.ValueObjects;
using SharedKernel.ValueObjects;

namespace Menu.Domain.Entities
{
    public sealed class MenuItemIngredient
    {
        public Guid Id { get; init; }
        public Guid IngredientId { get; init; }
        public IngredientName IngredientName { get; private set; }
        public MenuItemQuantity Quantity { get; init; }
        public bool IsOptional { get; private set; }
        public BrandOfTheIngredientInTheMenuItem Brand { get; private set; }

        /// <summary>
        /// Additional price applied when this ingredient is selected as optional.
        /// Null means no price impact.
        /// </summary>
        public Money AdditionalPrice { get; private set; }

        public MenuItemIngredient(Guid id, Guid ingredientId, IngredientName ingredientName, 
            MenuItemQuantity quantity, bool isOptional, BrandOfTheIngredientInTheMenuItem? brandSnapshot, 
            Money additionalPrice)
        {
            Id = id;
            IngredientId = ingredientId;
            IngredientName = ingredientName;
            Quantity = quantity;
            IsOptional = isOptional;

            Brand = brandSnapshot ?? BrandOfTheIngredientInTheMenuItem.Empty;

            AdditionalPrice = isOptional
                ? additionalPrice
                : Money.Zero(additionalPrice.Currency);

            Validate();
        }

        private void Validate()
        {
            if (!IsOptional && AdditionalPrice.Amount != 0)
            {
                throw new MenuException(
                    "Um preço adicional só pode ser definido para ingredientes opcionais.",
                    AdditionalPrice.ToString()
                );
            }
        }

        public Money CalculateAdditionalCost() => AdditionalPrice;
    }
}