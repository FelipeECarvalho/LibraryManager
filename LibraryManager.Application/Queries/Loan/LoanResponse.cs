namespace LibraryManager.Application.Queries.Loan
{
    using LibraryManager.Application.Queries.Book;
    using LibraryManager.Application.Queries.User;
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Enums;

    public sealed record LoanResponse
    {
        public Guid Id { get; init; }

        public BookResponse Book { get; init; }

        public UserResponse User { get; init; }

        public DateTimeOffset StartDate { get; init; }

        public DateTimeOffset EndDate { get; init; }

        public LoanStatus Status { get; init; }

        public static LoanResponse FromEntity(Loan loan) => new()
        {
            Id = loan.Id,
            EndDate = loan.EndDate,
            StartDate = loan.StartDate,
            Status = loan.Status,
            Book = BookResponse.FromEntity(loan.Book),
            User = UserResponse.FromEntity(loan.User)
        };
    }
}
