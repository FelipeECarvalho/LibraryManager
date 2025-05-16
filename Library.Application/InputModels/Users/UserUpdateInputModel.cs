namespace LibraryManager.Application.InputModels.Users
{
    using LibraryManager.Core.ValueObjects;

    public class UserUpdateInputModel
    {
        public Name Name { get; init; }

        public Address Address { get; init; }
    }
}
