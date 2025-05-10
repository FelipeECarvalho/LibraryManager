namespace Library.Persistence.Repositories
{
    using Library.Core.Entities;
    using Library.Core.Repositories;
    using Library.Persistence;
    using Microsoft.EntityFrameworkCore;

    public sealed class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryDbContext _context;

        public AuthorRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Author>> GetAllAsync()
        {
            return await _context.Authors
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            return await _context.Authors
                .Include(x => x.Books)
                .SingleOrDefaultAsync(x => x.Id == id);
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
