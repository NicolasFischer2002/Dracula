using SharedKernel.Exceptions;

namespace Menu.Domain.Exceptions
{
    public sealed class MenuException : CustomException<string>
    {
        public MenuException(string message, string invalidValue) 
            : base(message, invalidValue)
        {

        }

        public MenuException(string message, string invalidValue, Exception innerException) 
            : base(message, invalidValue, innerException)
        {

        }
    }
}