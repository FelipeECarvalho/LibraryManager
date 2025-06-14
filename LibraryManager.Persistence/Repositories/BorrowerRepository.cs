﻿namespace LibraryManager.Persistence.Repositories
{
    using LibraryManager.Core.Abstractions.Repositories;
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.ValueObjects;
    using LibraryManager.Persistence;
    using Microsoft.EntityFrameworkCore;

    internal sealed class BorrowerRepository
        : IBorrowerRepository
    {
        private readonly LibraryDbContext _context;

        public BorrowerRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Borrower>> GetAllAsync(int limit = 100, int offset = 1, CancellationToken cancellationToken = default)
        {
            return await _context.Borrowers
                .AsNoTracking()
                .OrderBy(x => x.CreateDate)
                .Skip((offset - 1) * limit)
                .Take(limit)
                .ToListAsync(cancellationToken);
        }

        public async Task<Borrower> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Borrowers
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<bool> IsEmailUnique(string email, CancellationToken cancellationToken = default)
        {
            return !await _context.Borrowers.AnyAsync(x => x.Email == email, cancellationToken);
        }

        public async Task<bool> IsDocumentUnique(string document, CancellationToken cancellationToken = default)
        {
            return !await _context.Borrowers.AnyAsync(x => x.Document == document, cancellationToken);
        }

        public void Add(Borrower borrower)
        {
            _context.Borrowers.Add(borrower);
        }
    }
}
