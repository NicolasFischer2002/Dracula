using SharedKernel.Formatters;
using SharedKernel.ValueObjects;

namespace Ordering.Application.Commands.PlaceOrder
{
    public sealed record CreateOrderRequest
    {
        public string Identifier { get; }
        public Currency Currency { get; }

        public CreateOrderRequest(string identifier, Currency currency)
        {
            Identifier = StringNormalizer.Normalize(identifier);
            Currency = currency;
        }
    }
}