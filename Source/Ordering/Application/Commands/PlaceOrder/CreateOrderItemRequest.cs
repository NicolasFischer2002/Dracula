using SharedKernel.Formatters;
using SharedKernel.ValueObjects;

namespace Ordering.Application.Commands.PlaceOrder
{
    public sealed record CreateOrderItemRequest
    {
        public Guid WaiterId { get; }
        public string NameItem { get; } 
        public Money GrossValue { get; }
        public Money Discount { get; }
        public string CookingInstructions { get; }

        public CreateOrderItemRequest(Guid waiterId, string nameItem, Money grossValue, Money discount, 
            string cookingInstructions)
        {
            WaiterId = waiterId;
            NameItem = StringNormalizer.Normalize(nameItem);
            GrossValue = grossValue;
            Discount = discount;
            CookingInstructions = cookingInstructions;
        }
    }
}