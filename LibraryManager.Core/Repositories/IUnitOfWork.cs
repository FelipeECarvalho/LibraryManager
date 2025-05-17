namespace LibraryManager.Core.Repositories
{
    public interface IUnitOfWork
    {
        IAuthorRepository Authors { get; }
        IBookRepository Books { get; }
        ILoanRepository Loans { get; }
        IUserRepository Users { get; }

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
