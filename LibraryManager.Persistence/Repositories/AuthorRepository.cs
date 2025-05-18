namespace LibraryManager.Persistence.Repositories
{
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Repositories;
    using LibraryManager.Persistence;
    using Microsoft.EntityFrameworkCore;

    internal sealed class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryDbContext _context;

        public AuthorRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Author>> GetAllAsync(CancellationToken ct)
        {
            return await _context.Authors
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task<Author> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return await _context.Authors
                .Include(x => x.Books)
                .SingleOrDefaultAsync(x => x.Id == id, ct);
        }

        public void Add(Author author)
        {
            _context.Authors.Add(author);
        }

        public void Update(Author author)
        {
            _context.Authors.Update(author);
        }
    }
}
