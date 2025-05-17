namespace LibraryManager.Core.ValueObjects
{
    public sealed record Name(string FirstName, string LastName)
    {
        public string FullName => $"{FirstName} {LastName}";

        public static Name FromFullName(string fullName)
        {
            var nameSplit = fullName.Split(' ', 2);
            var firstName = nameSplit[0];
            var lastName = nameSplit.Length > 1 ? nameSplit[1] : string.Empty;

            return new Name(firstName, lastName);
        }
    }
}
