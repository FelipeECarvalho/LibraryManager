namespace LibraryManager.Persistence.Configurations
{
    using LibraryManager.Core.Entities;
    using LibraryManager.Persistence.Constants;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal sealed class BorrowerConfiguration : IEntityTypeConfiguration<Borrower>
    {
        public void Configure(EntityTypeBuilder<Borrower> builder)
        {
            //builder.ToTable(TableNames.Borrowers);

            builder.Property(x => x.Document).HasMaxLength(30);

            builder.OwnsOne(x => x.Address, c =>
            {
                c.Property(a => a.Street).HasColumnName("Street").HasMaxLength(50).IsRequired();
                c.Property(a => a.Number).HasColumnName("Number").HasMaxLength(15).IsRequired();
                c.Property(a => a.District).HasColumnName("District").HasMaxLength(50).IsRequired();
                c.Property(a => a.City).HasColumnName("City").HasMaxLength(50).IsRequired();
                c.Property(a => a.State).HasColumnName("State").HasMaxLength(50).IsRequired();
                c.Property(a => a.CountryCode).HasColumnName("CountryCode").HasMaxLength(5).IsRequired();
                c.Property(a => a.ZipCode).HasColumnName("ZipCode").HasMaxLength(20).IsRequired();
                c.Property(a => a.Latitude).HasColumnName("Latitude").HasPrecision(9, 6).IsRequired(false);
                c.Property(a => a.Longitude).HasColumnName("Longitude").HasPrecision(9, 6).IsRequired(false);
                c.Property(a => a.Observation).HasColumnName("Observation").HasMaxLength(256).IsRequired(false);
            });

            builder.Navigation(x => x.Address).IsRequired(true);

            builder.HasOne(x => x.Library)
                .WithMany(x => x.Borrowers)
                .HasForeignKey(x => x.LibraryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.Email, x.LibraryId }).IsUnique();
            builder.HasIndex(x => new { x.Document, x.LibraryId }).IsUnique();
        }
    }
}
