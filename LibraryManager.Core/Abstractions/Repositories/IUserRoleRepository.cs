namespace LibraryManager.Core.Abstractions.Repositories
{
    using LibraryManager.Core.Entities;

    public interface IUserRoleRepository
    {
        Task<IList<UserRole>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
