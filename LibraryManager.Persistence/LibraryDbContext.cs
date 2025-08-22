namespace LibraryManager.Persistence
{
    using LibraryManager.Application.Models;
    using LibraryManager.Core.Entities;
    using Microsoft.EntityFrameworkCore;

    public sealed class LibraryDbContext(DbContextOptions<LibraryDbContext> options)
        : DbContext(options)
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<QueuedEmail> QueuedEmails { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryDbContext).Assembly);
    }
}
