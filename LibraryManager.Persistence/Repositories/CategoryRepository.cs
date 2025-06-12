namespace LibraryManager.Persistence.Repositories
{
    using LibraryManager.Core.Abstractions.Repositories;
    using LibraryManager.Core.Entities;
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

        public async Task<IList<Category>> GetAllAsync(int limit = 100, int offset = 1, CancellationToken cancellationToken = default)
        {
            return await _context.Categories
                .AsNoTracking()
                .OrderBy(x => x.CreateDate)
                .Skip((offset - 1) * limit)
                .Take(offset)
                .ToListAsync(cancellationToken);
        }

        public async Task<Category> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Categories
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public void Add(Category category)
        {
            _context.Categories.Add(category);
        }
    }
}
