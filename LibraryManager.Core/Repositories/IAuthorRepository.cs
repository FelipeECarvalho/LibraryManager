﻿namespace LibraryManager.Core.Repositories
{
    using LibraryManager.Core.Entities;

    public interface IAuthorRepository
    {
        Task<IList<Author>> GetAllAsync(int limit, int offset, CancellationToken ct = default);
        Task<Author> GetByIdAsync(Guid id, CancellationToken ct = default);

        void Add(Author author);
        void Update(Author author);
    }
}
