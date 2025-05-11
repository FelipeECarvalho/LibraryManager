namespace Library.Application.InputModels.Users
{
    using Library.Core.ValueObjects;

    public class UserUpdateInputModel
    {
        public Name Name { get; init; }

        public Address Address { get; init; }
    }
}
