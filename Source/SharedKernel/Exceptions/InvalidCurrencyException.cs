namespace SharedKernel.Exceptions
{
    public sealed class InvalidCurrencyException : Exception
    {
        public string? Currency { get; }

        public InvalidCurrencyException(string? currency)
            : base($"A moeda informada '{currency ?? "null"}' é inválida.")
        {
            Currency = currency;
        }
    }
}