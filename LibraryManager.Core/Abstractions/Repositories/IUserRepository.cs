namespace LibraryManager.Core.Abstractions.Repositories
{
    using LibraryManager.Core.Entities;

    public interface IUserRepository
    {
        Task<User> GetByEmail(string email, Guid libraryId, CancellationToken cancellationToken);
    }
}
