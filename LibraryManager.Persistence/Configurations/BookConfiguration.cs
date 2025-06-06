namespace LibraryManager.Persistence.Configurations
{
    using LibraryManager.Core.Entities;
    using LibraryManager.Persistence.Constants;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal sealed class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable(TableNames.Books);

            builder.Property(x => x.PublicationDate).IsRequired();
            builder.Property(x => x.StockNumber).IsRequired(false);
            builder.Property(x => x.Isbn).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Description).HasColumnType("text").IsRequired(false);

            builder.HasIndex(x => x.Title);
            builder.HasIndex(x => new { x.Isbn, x.LibraryId }).IsUnique();
        }
    }
}
