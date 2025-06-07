namespace LibraryManager.Persistence.Configurations
{
    using LibraryManager.Core.Entities.Users;
    using LibraryManager.Core.Enums;
    using LibraryManager.Persistence.Constants;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class UserConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.ToTable(TableNames.Users);

            builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
            builder.Property(x => x.PasswordHash).IsRequired().HasMaxLength(512);
            builder.Property(x => x.LastLogin);

            builder.OwnsOne(x => x.Name, c =>
            {
                c.Property(a => a.FirstName).HasColumnName("FirstName").HasMaxLength(100).IsRequired(true);
                c.Property(a => a.LastName).HasColumnName("LastName").HasMaxLength(100).IsRequired(true);
            });

            builder.Navigation(x => x.Name).IsRequired();

            builder.HasDiscriminator<UserType>(nameof(UserType))
                .HasValue<Borrower>(UserType.Borrower)
                .HasValue<Operator>(UserType.Operator);

            builder.HasIndex(u => u.Email)
                .IsUnique()
                .HasFilter($"[UserType] = 1");
        }
    }
}