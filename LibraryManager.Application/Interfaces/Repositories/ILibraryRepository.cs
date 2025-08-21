namespace LibraryManager.Application.Interfaces.Repositories
{
    using LibraryManager.Core.Entities;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ILibraryRepository
    {
        Task<Library> GetById(Guid id, CancellationToken cancellationToken = default);
    }
}
