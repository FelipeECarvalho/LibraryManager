namespace LibraryManager.Persistence.Configurations
{
    using LibraryManager.Core.Entities;
    using LibraryManager.Persistence.Constants;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal sealed class BookCategoryConfiguration : IEntityTypeConfiguration<BookCategory>
    {
        public void Configure(EntityTypeBuilder<BookCategory> builder)
        {
            builder.ToTable(TableNames.BookCategories);

            builder.HasKey(t => new { t.BookId, t.CategoryId });

            builder.HasOne(x => x.Book)
                .WithMany(x => x.BookCategories)
                .HasForeignKey(x => x.BookId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Category)
                .WithMany(x => x.BookCategories)
                .HasForeignKey(x => x.CategoryId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.BookId, x.CategoryId })
                .HasFilter("[IsDeleted] = 0")
                .IsUnique();
        }
    }
}
