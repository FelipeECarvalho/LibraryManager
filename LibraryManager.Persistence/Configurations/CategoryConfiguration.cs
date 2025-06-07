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

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id).IsRequired().ValueGeneratedNever();
            builder.Property(x => x.CreateDate).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired().HasDefaultValue(false);

            builder.Property(x => x.Name).HasMaxLength(50);
            builder.Property(x => x.Description).HasMaxLength(256).IsRequired(false);

            builder.HasOne(x => x.Library)
                .WithMany(x => x.Categories)
                .HasForeignKey(x => x.LibraryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.Name, x.LibraryId }).IsUnique();

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
