namespace LibraryManager.Application.Queries.Borrower
{
    using LibraryManager.Core.ValueObjects;

    public sealed record BorrowerResponse
    {
        public Guid Id { get; init; }

        public Name Name { get; init; }

        public string Document { get; init; }

        public string Email { get; init; }

        public Address Address { get; set; }

        public static BorrowerResponse FromEntity(Core.Entities.Borrower borrower)
            => new()
            {
                Id = borrower.Id,
                Name = borrower.Name,
                Document = borrower.Document,
                Email = borrower.Email,
                Address = borrower.Address
            };
    }
}
