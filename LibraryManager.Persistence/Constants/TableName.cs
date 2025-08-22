namespace LibraryManager.Persistence.Constants
{
    using LibraryManager.Application.Models;
    using LibraryManager.Core.Entities;

    internal static class TableNames
    {
        internal const string Books = nameof(Book);

        internal const string Borrowers = nameof(Borrower);

        internal const string Loans = nameof(Loan);

        internal const string Authors = nameof(Author);

        internal const string Categories = nameof(Category);

        internal const string Libraries = nameof(Library);

        internal const string Users = nameof(User);

        internal const string Roles = nameof(Role);

        internal const string UserRoles = nameof(UserRole);

        internal const string BookCategories = nameof(BookCategory);

        internal const string QueuedEmails = nameof(QueuedEmail);

        internal const string RefreshTokens = nameof(RefreshToken);
    }
}
