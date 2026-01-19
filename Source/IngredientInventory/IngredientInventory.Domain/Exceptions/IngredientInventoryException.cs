using SharedKernel.Exceptions;

namespace IngredientInventory.Domain.Exceptions
{
    public class IngredientInventoryException : CustomException<string>
    {
        public IngredientInventoryException(string message) : base(message) { }
        public IngredientInventoryException(string message, string invalidValue) : base(message, invalidValue) { }
        public IngredientInventoryException(string message, Exception innerException) : base(message, innerException) { }
        public IngredientInventoryException(string message, string invalidValue, Exception innerException)
            : base(message, invalidValue, innerException) { }

        public static void ThrowIf(bool condition, string invalidValue, string message)
        {
            if (condition)
                throw new IngredientInventoryException(message, invalidValue);
        }
        public static void ThrowIf(bool condition, string message)
        {
            if (condition)
                throw new IngredientInventoryException(message);
        }
        public static void ThrowIfNull<TValue>(TValue? value, string message) where TValue : class
        {
            if (value is null)
                throw new IngredientInventoryException(message);
        }
    }
}