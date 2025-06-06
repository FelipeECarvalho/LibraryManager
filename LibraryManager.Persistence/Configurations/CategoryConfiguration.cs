namespace LibraryManager.Persistence.Configurations
{
    using LibraryManager.Core.Entities;
    using LibraryManager.Persistence.Constants;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(TableNames.Categories);

            builder.Property(x => x.Name).HasMaxLength(50);
            builder.Property(x => x.Description).HasMaxLength(256).IsRequired(false);

            builder.HasIndex(x => new { x.Name, x.LibraryId }).IsUnique();
        }
    }
}
