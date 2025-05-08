using Library.Core.Entities;
using Library.Core.Interfaces.Repositories;

namespace Library.Infrastructure.Persistence.Repositories
{
    public class AuthorRepository  : BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(LibraryDbContext context) 
            : base(context) 
        { 
        }
    }
}
