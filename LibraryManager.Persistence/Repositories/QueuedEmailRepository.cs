namespace LibraryManager.Persistence.Repositories
{
    using LibraryManager.Application.Abstractions.Repositories;
    using LibraryManager.Application.Models;
    using Microsoft.EntityFrameworkCore;
    using System;

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

        public async Task<QueuedEmail> GetByIdAsync(Guid id)
        {
            return await _context.QueuedEmails
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
