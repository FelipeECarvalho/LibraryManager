namespace LibraryManager.Persistence.Configurations
{
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Enums;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System.Reflection.Emit;

    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id).IsRequired().ValueGeneratedNever();
            builder.Property(x => x.CreateDate).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired().HasDefaultValue(false);

            builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
            builder.Property(x => x.PasswordHash).IsRequired().HasMaxLength(512);
            builder.Property(x => x.LastLogin);

            builder.OwnsOne(x => x.Name, c =>
            {
                c.Property(a => a.FirstName).HasColumnName("FirstName").HasMaxLength(100).IsRequired(true);
                c.Property(a => a.LastName).HasColumnName("LastName").HasMaxLength(100).IsRequired(true);
            });

            builder.Navigation(x => x.Name).IsRequired();

            builder
                .HasDiscriminator<UserType>(nameof(UserType))
                .HasValue<Borrower>(UserType.Borrower)
                .HasValue<Operator>(UserType.Operator);

            builder
                .HasIndex(u => u.Email)
                .IsUnique()
                .HasFilter($"[UserType] = 1");

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}