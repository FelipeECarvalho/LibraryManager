namespace LibraryManager.Persistence.Repositories
{
    using LibraryManager.Core.Entities.Users;
    using LibraryManager.Core.Repositories;
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

        public async Task<IList<Borrower>> GetAllAsync(int limit, int offset, CancellationToken ct)
        {
            return await _context.Borrowers
                .AsNoTracking()
                .OrderBy(x => x.CreateDate)
                .Skip((offset - 1) * limit)
                .Take(limit)
                .ToListAsync(ct);
        }

        public async Task<Borrower> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return await _context.Borrowers
                .SingleOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<bool> IsEmailUnique(string email, CancellationToken ct)
        {
            return !await _context.Borrowers.AnyAsync(x => x.Email == email, ct);
        }

        public async Task<bool> IsDocumentUnique(string document, CancellationToken ct)
        {
            return !await _context.Borrowers.AnyAsync(x => x.Document == document, ct);
        }

        public void Add(Borrower borrower)
        {
            _context.Borrowers.Add(borrower);
        }

        public void Update(Borrower borrower)
        {
            _context.Borrowers.Update(borrower);
        }
    }
}
