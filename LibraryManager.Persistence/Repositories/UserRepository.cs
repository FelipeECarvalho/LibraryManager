namespace LibraryManager.Persistence.Repositories
{
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Repositories;
    using LibraryManager.Persistence;
    using Microsoft.EntityFrameworkCore;

    internal sealed class UserRepository : IUserRepository
    {
        private readonly LibraryDbContext _context;

        public UserRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IList<User>> GetAllAsync(CancellationToken ct)
        {
            return await _context.Users
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task<User> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return await _context.Users
                .SingleOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<bool> IsEmailUnique(string email, CancellationToken ct)
        {
            return !await _context.Users.AnyAsync(x => x.Email == email, ct);
        }

        public async Task<bool> IsDocumentUnique(string document, CancellationToken ct)
        {
            return !await _context.Users.AnyAsync(x => x.Document == document, ct);
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
        }
    }
}
