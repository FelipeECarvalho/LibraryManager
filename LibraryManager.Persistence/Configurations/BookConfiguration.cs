﻿namespace LibraryManager.Persistence.Configurations
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

            builder.HasKey(x => x.Id);

            builder.Property(a => a.Id).ValueGeneratedNever();
            builder.Property(x => x.CreateDate);
            builder.Property(x => x.UpdateDate);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

            builder.Property(x => x.PublicationDate);
            builder.Property(x => x.StockNumber).IsRequired(false);
            builder.Property(x => x.ISBN).HasMaxLength(50);
            builder.Property(x => x.Title).HasMaxLength(100);
            builder.Property(x => x.Description).HasColumnType("text").IsRequired(false);

            builder.HasIndex(x => x.Title);
            builder.HasIndex(x => x.ISBN).IsUnique();

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
