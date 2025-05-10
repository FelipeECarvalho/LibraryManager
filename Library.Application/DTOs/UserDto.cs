namespace Library.Application.DTOs
{
    using Library.Core.ValueObjects;

    public class UserDto
    {
        public Guid Guid { get; init; }

        public Name Name{ get; init; }

        public string Document { get; init; }

        public string Email { get; init; }

        public Address Address{ get; set; }
    }
}
