namespace LibraryManager.Persistence.Constants
{
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Entities.Users;

    internal static class TableNames
    {
        internal const string Books = nameof(Book);

        internal const string Loans = nameof(Loan);

        internal const string Authors = nameof(Author);

        internal const string Categories = nameof(Category);

        internal const string Libraries = nameof(Library);

        internal const string Users = nameof(User);

        internal const string BookCategory = nameof(Core.Entities.BookCategory);
    }
}
