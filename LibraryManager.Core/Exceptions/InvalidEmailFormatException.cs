namespace LibraryManager.Core.Exceptions
{
    using System;

    public class InvalidEmailFormatException : Exception
    {
        public InvalidEmailFormatException(string email) : base($"Invalid email format '{email}'.")
        {
        }
    }
}
