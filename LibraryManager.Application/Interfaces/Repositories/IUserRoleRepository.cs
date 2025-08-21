namespace LibraryManager.Application.Interfaces.Repositories
{
    using LibraryManager.Core.Entities;

    public interface IUserRoleRepository
    {
        Task<IList<UserRole>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
