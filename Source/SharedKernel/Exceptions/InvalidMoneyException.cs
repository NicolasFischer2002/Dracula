namespace SharedKernel.Exceptions
{
    public sealed class InvalidMoneyException : Exception
    {
        public decimal Amount { get; }

        public InvalidMoneyException(string message, decimal amount)
            : base(message)
        {
            Amount = amount;
        }
    }
}