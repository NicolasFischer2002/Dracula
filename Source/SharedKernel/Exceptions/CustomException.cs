namespace SharedKernel.Exceptions
{
    public abstract class CustomException<T> : Exception
    {
        public T? InvalidValue { get; protected set; }

        protected CustomException(string message) : base(message) { }

        protected CustomException(string message, T invalidValue) : base(message)
        {
            InvalidValue = invalidValue;
        }

        protected CustomException(string message, Exception innerException)
            : base(message, innerException) { }

        protected CustomException(string message, T invalidValue, Exception innerException)
            : base(message, innerException)
        {
            InvalidValue = invalidValue;
        }
    }
}