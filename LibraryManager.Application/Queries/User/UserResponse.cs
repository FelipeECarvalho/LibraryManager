namespace LibraryManager.Application.Queries.User
{
    using LibraryManager.Core.ValueObjects;

    public class UserResponse
    {
        public Guid Id { get; init; }

        public Name Name { get; init; }

        public string Document { get; init; }

        public string Email { get; init; }

        public Address Address { get; set; }

        public static UserResponse FromEntity(Core.Entities.User user) 
            => new()
        {
            Id = user.Id,
            Name = user.Name,
            Document = user.Document,
            Email = user.Email,
            Address = user.Address
        };
    }
}
