using Library.Core.Entities;
using Library.Core.Interfaces.Repositories;

namespace Library.Infrastructure.Persistence.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(LibraryDbContext context)
            : base(context)
        {
        }
    }
}
