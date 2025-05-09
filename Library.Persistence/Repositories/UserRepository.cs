using Library.Core.Entities;
using Library.Core.Repositories;
using Library.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.Repositories
{
    public sealed class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(LibraryDbContext context)
            : base(context)
        {
        }

        public async Task<IList<User>> GetAllAsync()
        {
            return await _context.Users
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
