﻿namespace LibraryManager.Core.Repositories
{
    using LibraryManager.Core.Entities;

    public interface ICategoryRepository
    {
        Task<IList<Category>> GetAllAsync(CancellationToken ct = default);
        Task<Category> GetById(Guid id, CancellationToken ct = default);

        public void Add(Category category);
        public void Update(Category category);
    }
}
