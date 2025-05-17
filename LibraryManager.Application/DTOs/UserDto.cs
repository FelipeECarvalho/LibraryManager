namespace LibraryManager.Application.DTOs
{
    using LibraryManager.Core.ValueObjects;

    public class UserDto
    {
        public Guid Id { get; init; }

        public Name Name { get; init; }

        public string Document { get; init; }

        public string Email { get; init; }

        public Address Address { get; set; }
    }
}
