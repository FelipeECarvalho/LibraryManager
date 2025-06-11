namespace LibraryManager.Application.Queries.Borrower
{
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.ValueObjects;

    public sealed record BorrowerResponse
    {
        public Guid Id { get; init; }

        public Name Name { get; init; }

        public string Document { get; init; }

        public string Email { get; init; }

        public Guid LibraryId { get; init; }

        public Address Address { get; set; }

        public static BorrowerResponse FromEntity(Borrower borrower)
            => new()
            {
                Id = borrower.Id,
                Name = borrower.Name,
                Document = borrower.Document,
                Email = borrower.Email.ToString(),
                Address = borrower.Address,
                LibraryId = borrower.LibraryId
            };
    }
}
