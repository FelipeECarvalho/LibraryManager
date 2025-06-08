namespace LibraryManager.Core.ValueObjects
{
    using System;
    using System.Collections;
    using System.Text.RegularExpressions;

    public sealed record Email : IEqualityComparer
    {
        public string Address { get; }

        public Email(string address)
        {
            if (string.IsNullOrWhiteSpace(address) || !Regex.IsMatch(address, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new ArgumentException("Invalid email format.", nameof(address));
            }

            Address = address;
        }

        public override string ToString()
        {
            return Address;
        }

        public new bool Equals(object? x, object? y)
        {
            if (x is null && y is null)
            {
                return true;
            }

            if (x is null || y is null)
            {
                return false;
            }

            return x.Equals(y);
        }

        public int GetHashCode(object obj)
        {
            if (obj == null)
            {
                return 0;
            }

            return obj.GetHashCode();
        }
    }
}
