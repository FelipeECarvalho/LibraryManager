namespace Library.Persistence
{
    using Library.Core.Entities;
    using Microsoft.EntityFrameworkCore;

    public class LibraryDbContext(DbContextOptions<LibraryDbContext> options) : DbContext(options)
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(e =>
            {
                e.HasKey(a => a.Id);

                e.Property(x => x.Guid);
                e.Property(x => x.CreateDate);
                e.Property(x => x.UpdateDate);
                e.Property(x => x.IsDeleted).HasDefaultValue(false);

                e.Property(x => x.Description).HasMaxLength(256).IsRequired(false);
                e.Property(x => x.Name.FirstName).HasMaxLength(100);
                e.Property(x => x.Name.LastName).HasMaxLength(100);
                
                e.HasMany(x => x.Books)
                    .WithOne(x => x.Author)
                    .HasForeignKey(x => x.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasQueryFilter(x => !x.IsDeleted);
            });

            modelBuilder.Entity<User>(e =>
            {
                e.HasKey(a => a.Id);

                e.Property(x => x.Guid);
                e.Property(x => x.CreateDate);
                e.Property(x => x.UpdateDate);
                e.Property(x => x.IsDeleted).HasDefaultValue(false);

                e.Property(x => x.Name.FirstName).HasMaxLength(100);
                e.Property(x => x.Name.LastName).HasMaxLength(100);
                e.Property(x => x.Document).HasMaxLength(30);
                e.Property(x => x.Email).HasMaxLength(50);

                e.Property(x => x.Address.Street).HasMaxLength(50);
                e.Property(x => x.Address.Number).HasMaxLength(15);
                e.Property(x => x.Address.District).HasMaxLength(50);
                e.Property(x => x.Address.City).HasMaxLength(50);
                e.Property(x => x.Address.State).HasMaxLength(50);
                e.Property(x => x.Address.CountryCode).HasMaxLength(5);
                e.Property(x => x.Address.ZipCode).HasMaxLength(20);

                e.HasQueryFilter(x => !x.IsDeleted);
            });

            modelBuilder.Entity<Book>(e =>
            {
                e.Property(x => x.Guid);
                e.Property(x => x.CreateDate);
                e.Property(x => x.UpdateDate);
                e.Property(x => x.IsDeleted).HasDefaultValue(false);

                e.Property(x => x.PublicationDate);
                e.Property(x => x.StockNumber).IsRequired(false);
                e.Property(x => x.ISBN).HasMaxLength(50);
                e.Property(x => x.Title).HasMaxLength(100);
                e.Property(x => x.Description).HasMaxLength(256).IsRequired(false);

                e.HasQueryFilter(x => !x.IsDeleted);
            });

            modelBuilder.Entity<Loan>(e =>
            {
                e.Property(x => x.Guid);
                e.Property(x => x.CreateDate);
                e.Property(x => x.UpdateDate);
                e.Property(x => x.IsDeleted).HasDefaultValue(false);

                e.Property(x => x.StartDate);
                e.Property(x => x.EndDate);
                e.Property(x => x.IsReturned).HasDefaultValue(false);

                e.HasOne(l => l.User)
                    .WithMany()
                    .HasForeignKey(l => l.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(l => l.Book)
                    .WithMany()
                    .HasForeignKey(l => l.BookId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasQueryFilter(x => !x.IsDeleted);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
