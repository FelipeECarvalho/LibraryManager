namespace LibraryManager.Application.Interfaces.Repositories
{
    using LibraryManager.Core.Entities;

    public interface IUserRepository
    {
        Task<User> GetByEmail(string email, Guid libraryId, CancellationToken cancellationToken);
    }
}
