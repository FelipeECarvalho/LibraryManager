namespace LibraryManager.Persistence.Repositories
{
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Interfaces.Repositories;
    using LibraryManager.Persistence;
    using Microsoft.EntityFrameworkCore;

    internal sealed class AuthorRepository
        : IAuthorRepository
    {
        private readonly LibraryDbContext _context;

        public AuthorRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Author>> GetAllAsync(int limit = 100, int offset = 1, CancellationToken cancellationToken = default)
        {
            return await _context.Authors
                .AsNoTracking()
                .OrderBy(x => x.CreateDate)
                .Skip((offset - 1) * limit)
                .Take(limit)
                .ToListAsync(cancellationToken);
        }

        public async Task<Author> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Authors
                .Include(x => x.Books)
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public void Add(Author author)
        {
            _context.Authors.Add(author);
        }
    }
}
