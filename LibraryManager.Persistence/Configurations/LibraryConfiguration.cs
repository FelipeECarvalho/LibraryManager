namespace LibraryManager.Persistence.Configurations
{
    using LibraryManager.Core.Entities;
    using LibraryManager.Persistence.Constants;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal sealed class LibraryConfiguration : IEntityTypeConfiguration<Library>
    {
        public void Configure(EntityTypeBuilder<Library> builder)
        {
            builder.ToTable(TableNames.Libraries);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.OpeningTime);
            builder.Property(x => x.ClosingTime);

            builder.OwnsOne(x => x.Address, c =>
            {
                c.Property(a => a.Street).HasColumnName("Street").HasMaxLength(50).IsRequired();
                c.Property(a => a.Number).HasColumnName("Number").HasMaxLength(15).IsRequired();
                c.Property(a => a.District).HasColumnName("District").HasMaxLength(50).IsRequired();
                c.Property(a => a.City).HasColumnName("City").HasMaxLength(50).IsRequired();
                c.Property(a => a.State).HasColumnName("State").HasMaxLength(50).IsRequired();
                c.Property(a => a.CountryCode).HasColumnName("CountryCode").HasMaxLength(5).IsRequired();
                c.Property(a => a.ZipCode).HasColumnName("ZipCode").HasMaxLength(20).IsRequired();
                c.Property(a => a.Latitude).HasColumnName("Latitude").HasPrecision(9, 6).IsRequired(false).IsRequired();
                c.Property(a => a.Longitude).HasColumnName("Longitude").HasPrecision(9, 6).IsRequired(false).IsRequired();
                c.Property(a => a.Observation).HasColumnName("Observation").HasMaxLength(256).IsRequired(false).IsRequired();
            });

            builder.Navigation(x => x.Address).IsRequired();

            builder.HasMany(x => x.Borrowers)
                .WithOne(x => x.Library)
                .HasForeignKey(x => x.LibraryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Books)
                .WithOne(x => x.Library)
                .HasForeignKey(x => x.LibraryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Categories)
                .WithOne(x => x.Library)
                .HasForeignKey(x => x.LibraryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
