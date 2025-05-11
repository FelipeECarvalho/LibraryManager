namespace Library.Persistence.Configurations
{
    using Library.Core.Entities;
    using Library.Persistence.Constants;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal sealed class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable(TableNames.Authors);

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id).ValueGeneratedNever();
            builder.Property(x => x.CreateDate);
            builder.Property(x => x.UpdateDate);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

            builder.Property(x => x.Description).HasMaxLength(256).IsRequired(false);
            builder.OwnsOne(x => x.Name, c =>
            {
                c.Property(a => a.FirstName).HasColumnName("FirstName").HasMaxLength(100);
                c.Property(a => a.LastName).HasColumnName("LastName").HasMaxLength(100);
            });

            builder.HasMany(x => x.Books)
                .WithOne(x => x.Author)
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
