using Library.Core.Entities;
using Library.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Persistence.Repositories
{
    public sealed class AuthorRepository  : BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(LibraryDbContext context) 
            : base(context) 
        {
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
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
