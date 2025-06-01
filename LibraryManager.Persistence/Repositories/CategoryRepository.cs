namespace LibraryManager.Persistence.Repositories
{
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Repositories;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class CategoryRepository
        : ICategoryRepository
    {
        private readonly LibraryDbContext _context;

        public CategoryRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Category>> GetAllAsync(int limit, int offset, CancellationToken ct)
        {
            return await _context.Categories
                .AsNoTracking()
                .Skip((offset - 1) * limit)
                .Take(offset)
                .ToListAsync(ct);
        }

        public async Task<Category> GetById(Guid id, CancellationToken ct)
        {
            return await _context.Categories
                .SingleOrDefaultAsync(x => x.Id == id, ct);
        }

        public void Add(Category category)
        {
            _context.Categories.Add(category);
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }
    }
}
