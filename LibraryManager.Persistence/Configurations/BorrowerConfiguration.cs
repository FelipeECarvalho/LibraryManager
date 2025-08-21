namespace LibraryManager.Persistence.Configurations
{
    using LibraryManager.Core.Entities;
    using LibraryManager.Persistence.Constants;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal sealed class BorrowerConfiguration
        : BaseEntityConfiguration<Borrower>
    {
        public override void Configure(EntityTypeBuilder<Borrower> builder)
        {
            base.Configure(builder);

            builder.ToTable(TableNames.Borrowers);

            builder.Property(x => x.Document).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(256).IsRequired();

            builder.OwnsOne(x => x.Name, c =>
            {
                c.Property(a => a.FirstName).HasColumnName("FirstName").HasMaxLength(100).IsRequired();
                c.Property(a => a.LastName).HasColumnName("LastName").HasMaxLength(100).IsRequired();
            });

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

            builder.HasOne(x => x.Library)
                .WithMany(x => x.Borrowers)
                .HasForeignKey(x => x.LibraryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.Document, x.LibraryId })
                .HasFilter("[IsDeleted] = 0")
                .IsUnique();

            builder.HasIndex(x => x.Email);
            builder.HasIndex(x => new { x.Email, x.LibraryId })
                 .HasFilter("[IsDeleted] = 0")
                 .IsUnique();
        }
    }
}
