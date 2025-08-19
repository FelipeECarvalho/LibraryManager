namespace LibraryManager.Persistence.Repositories
{
    using LibraryManager.Core.Abstractions.Repositories;

    internal sealed class QueuedEmailRepository : IQueuedEmailRepository
    {
        private readonly LibraryDbContext _context;

        public QueuedEmailRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public void AddAsync()
        {
            throw new NotImplementedException();
        }
    }
}
