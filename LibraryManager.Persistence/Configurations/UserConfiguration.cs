namespace LibraryManager.Persistence.Configurations
{
    using LibraryManager.Core.Entities;
    using LibraryManager.Persistence.Constants;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal sealed class UserConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.ToTable(TableNames.Users);

            builder.Property(x => x.PasswordHash).IsRequired().HasMaxLength(512);
            builder.Property(x => x.Role).IsRequired();
            builder.Property(x => x.LastLogin).IsRequired(false);

            builder.OwnsOne(x => x.Email, c =>
            {
                c.Property(a => a.Address).HasColumnName("Email").HasMaxLength(256).IsRequired();
                c.HasIndex(x => x.Address);
            });

            builder.OwnsOne(x => x.Name, c =>
            {
                c.Property(a => a.FirstName).HasColumnName("FirstName").HasMaxLength(100).IsRequired();
                c.Property(a => a.LastName).HasColumnName("LastName").HasMaxLength(100).IsRequired();
            });

            builder.HasOne(x => x.Library)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.LibraryId)
                .OnDelete(DeleteBehavior.Restrict);

            //builder.HasIndex("Email", "LibraryId")
            //    .HasFilter("[IsDeleted] = 0")
            //    .IsUnique();
        }
    }
}