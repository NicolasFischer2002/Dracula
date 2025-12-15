namespace Ordering.Application.Commands.PlaceOrder
{
    public sealed record CreateOrderRequest
    {
        public string Identifier { get; init; }

        public CreateOrderRequest(string identifier)
        {
            Identifier = identifier;
        }
    }
}