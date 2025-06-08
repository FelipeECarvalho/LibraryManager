namespace LibraryManager.Persistence.Repositories
{
    using LibraryManager.Core.Entities.Users;
    using LibraryManager.Core.Repositories;
    using Microsoft.EntityFrameworkCore;

    internal sealed class UserRepository : IUserRepository
    {
        private readonly LibraryDbContext _context;

        public UserRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByEmail(string email, CancellationToken ct)
        {
            return await _context.Users
                .SingleOrDefaultAsync(x => x.Email.Equals(email), ct);
        }

        public void Update(User user)
        {
            _context.Update(user);
        }
    }
}
