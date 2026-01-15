namespace SharedKernel.Exceptions
{
    public sealed class InvalidMoneyException : CustomException<decimal>
    {
        public InvalidMoneyException(string message) : base(message) { }
        public InvalidMoneyException(string message, decimal invalidValue) : base(message, invalidValue) { }
        public InvalidMoneyException(string message, Exception innerException) : base(message, innerException) { }
        public InvalidMoneyException(string message, decimal invalidValue, Exception innerException)
            : base(message, invalidValue, innerException) { }

        public InvalidMoneyException(decimal amount)
            : base($"Valor monetário inválido: {amount}", amount) { }

        public static void ThrowIf(bool condition, decimal invalidValue, string message)
        {
            if (condition)
                throw new InvalidMoneyException(message, invalidValue);
        }

        public static void ThrowIf(bool condition, string message)
        {
            if (condition)
                throw new InvalidMoneyException(message);
        }

        public static void ThrowIfNull<TValue>(TValue? value, string message) where TValue : class
        {
            if (value is null)
                throw new InvalidMoneyException(message);
        }

        public static void ThrowIfNotPositive(decimal amount, string paramName)
        {
            if (amount <= 0)
                throw new InvalidMoneyException($"Valor '{paramName}' deve ser positivo.", amount);
        }

        public static void ThrowIfNegative(decimal amount, string paramName)
        {
            if (amount < 0)
                throw new InvalidMoneyException($"Valor '{paramName}' não pode ser negativo.", amount);
        }
    }
}