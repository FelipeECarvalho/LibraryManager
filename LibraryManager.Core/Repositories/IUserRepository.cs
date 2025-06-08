namespace LibraryManager.Core.Repositories
{
    using LibraryManager.Core.Entities.Users;

    public interface IUserRepository
    {
        Task<User> GetByEmail(string email, CancellationToken ct);

        void Update(User user);
    }
}
