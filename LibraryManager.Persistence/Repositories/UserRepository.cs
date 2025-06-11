namespace LibraryManager.Persistence.Repositories
{
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Interfaces.Repositories;
    using LibraryManager.Core.ValueObjects;
    using Microsoft.EntityFrameworkCore;

    internal sealed class UserRepository : IUserRepository
    {
        private readonly LibraryDbContext _context;

        public UserRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByEmail(string email, Guid libraryId, CancellationToken cancellationToken)
        {
            var normalizedEmailToFind = new Email(email.ToLower());

            return await _context.Users
                .SingleOrDefaultAsync(x => x.Email == normalizedEmailToFind && x.LibraryId == libraryId, cancellationToken);
        }
    }
}
