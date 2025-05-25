namespace LibraryManager.Persistence.Configurations
{
    using LibraryManager.Core.Entities;
    using LibraryManager.Persistence.Constants;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(TableNames.Users);

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id).ValueGeneratedNever();
            builder.Property(x => x.CreateDate);
            builder.Property(x => x.UpdateDate);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

            builder.Property(x => x.Document).HasMaxLength(30);
            builder.Property(x => x.Email).HasMaxLength(50);

            builder.OwnsOne(x => x.Name, c =>
            {
                c.Property(a => a.FirstName).HasColumnName("FirstName").HasMaxLength(100);
                c.Property(a => a.LastName).HasColumnName("LastName").HasMaxLength(100);
            });

            builder.OwnsOne(x => x.Address, c =>
            {
                c.Property(a => a.Street).HasColumnName("Street").HasMaxLength(50);
                c.Property(a => a.Number).HasColumnName("Number").HasMaxLength(15);
                c.Property(a => a.District).HasColumnName("District").HasMaxLength(50);
                c.Property(a => a.City).HasColumnName("City").HasMaxLength(50);
                c.Property(a => a.State).HasColumnName("State").HasMaxLength(50);
                c.Property(a => a.CountryCode).HasColumnName("CountryCode").HasMaxLength(5);
                c.Property(a => a.ZipCode).HasColumnName("ZipCode").HasMaxLength(20);
            });

            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.Document).IsUnique();

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
