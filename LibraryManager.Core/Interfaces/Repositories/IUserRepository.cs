namespace LibraryManager.Core.Interfaces.Repositories
{
    using LibraryManager.Core.Entities.Users;

    public interface IUserRepository
    {
        Task<User> GetByEmail(string email, CancellationToken cancellationToken);

        void Update(User user);
    }
}
