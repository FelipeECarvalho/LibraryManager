namespace LibraryManager.Persistence.Repositories
{
    using LibraryManager.Application.Abstractions.Repositories;
    using LibraryManager.Application.Models;

    internal sealed class QueuedEmailRepository
        : IQueuedEmailRepository
    {
        private readonly LibraryDbContext _context;

        public QueuedEmailRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public void Add(QueuedEmail queuedEmail)
        {
            _context.QueuedEmails.Add(queuedEmail);
        }
    }
}
