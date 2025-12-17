namespace Ordering.Application.Commands.PlaceOrder
{
    public sealed record CreateOrderItemRequest
    {
        public Guid WaiterId { get; init; }
        public string NameItem { get; init; } 
        public decimal GrossValue { get; init; }
        public decimal Discount { get; init; }
        public string CookingInstructions { get; init; }

        public CreateOrderItemRequest(Guid waiterId, string nameItem, decimal grossValue, decimal discount, 
            string cookingInstructions)
        {
            WaiterId = waiterId;
            NameItem = nameItem;
            GrossValue = grossValue;
            Discount = discount;
            CookingInstructions = cookingInstructions;
        }
    }
}