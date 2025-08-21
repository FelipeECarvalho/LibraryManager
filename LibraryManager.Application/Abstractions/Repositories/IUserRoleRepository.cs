namespace LibraryManager.Application.Abstractions.Repositories
{
    using LibraryManager.Core.Entities;

    public interface IUserRoleRepository
    {
        Task<IList<UserRole>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
