namespace LibraryManager.Core.Exceptions
{
    using System;

    public class InvalidEmailException : Exception
    {
        public InvalidEmailException(string email) : base($"Invalid email format '{email}'.")
        {
        }
    }
}
