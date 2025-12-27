namespace SharedKernel.Exceptions
{
    public sealed class InvalidUtcInstantException : Exception
    {
        public DateTime ProvidedValue { get; }

        public InvalidUtcInstantException(DateTime providedValue)
            : base($"A data e hora fornecidas devem estar em UTC. Tipo: {providedValue.Kind}.")
        {
            ProvidedValue = providedValue;
        }
    }
}