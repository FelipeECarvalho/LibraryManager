using Library.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Persistence
{
    public class LibraryDbContext(DbContextOptions<LibraryDbContext> options) : DbContext(options)
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasQueryFilter(x => !x.IsDeleted);

            modelBuilder.Entity<User>()
                .HasQueryFilter(x => !x.IsDeleted);

            modelBuilder.Entity<Book>()
                .HasQueryFilter(x => !x.IsDeleted);

            base.OnModelCreating(modelBuilder);
        }
    }
}
