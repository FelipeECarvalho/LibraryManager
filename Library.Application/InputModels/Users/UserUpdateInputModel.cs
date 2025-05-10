namespace Library.Application.InputModels.Users
{
    using Library.Core.ValueObjects;

    public class UserUpdateInputModel
    {
        public Name Name { get; set; }

        public Address Address { get; set; }
    }
}
