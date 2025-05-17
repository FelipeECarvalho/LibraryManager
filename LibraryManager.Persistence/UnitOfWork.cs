namespace LibraryManager.Persistence
{
    using LibraryManager.Core.Repositories;

    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryDbContext _context;

        public IAuthorRepository Authors { get; private set; }

        public IBookRepository Books { get; private set; }

        public ILoanRepository Loans { get; private set; }

        public IUserRepository Users { get; private set; }

        public UnitOfWork(
            LibraryDbContext context,
            IAuthorRepository authorRepository,
            IBookRepository bookRepository,
            ILoanRepository loanRepository,
            IUserRepository userRepository)
        {
            _context = context;
            Authors = authorRepository;
            Books = bookRepository;
            Loans = loanRepository;
            Users = userRepository;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
