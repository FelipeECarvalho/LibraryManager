namespace LibraryManager.Persistence.Repositories
{
    using LibraryManager.Application.Abstractions.Repositories;
    using LibraryManager.Core.Entities;
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

        public async Task<IList<Borrower>> GetAllAsync(Guid libraryId, int pageSize = 100, int pageNumber = 1, CancellationToken cancellationToken = default)
        {
            return await _context.Borrowers
                .AsNoTracking()
                .OrderBy(x => x.CreateDate)
                .Where(x => x.LibraryId == libraryId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<Borrower> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Borrowers
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<bool> IsEmailUnique(string email, Guid libraryId, CancellationToken cancellationToken = default)
        {
            return !await _context.Borrowers.AnyAsync(x => x.Email == email && x.LibraryId == libraryId, cancellationToken);
        }

        public async Task<bool> IsDocumentUnique(string document, Guid libraryId, CancellationToken cancellationToken = default)
        {
            return !await _context.Borrowers.AnyAsync(x => x.Document == document && x.LibraryId == libraryId, cancellationToken);
        }

        public void Add(Borrower borrower)
        {
            _context.Borrowers.Add(borrower);
        }
    }
}
