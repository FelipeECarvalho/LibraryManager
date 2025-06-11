namespace LibraryManager.Core.ValueObjects
{
    using LibraryManager.Core.Exceptions;
    using System.Text.RegularExpressions;

    public sealed record Email
    {
        public string Address { get; }

        public Email(string address)
        {
            if (string.IsNullOrWhiteSpace(address) || !Regex.IsMatch(address, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new InvalidEmailException(address);
            }

            Address = address;
        }

        public override string ToString()
            => Address;
    }
}
