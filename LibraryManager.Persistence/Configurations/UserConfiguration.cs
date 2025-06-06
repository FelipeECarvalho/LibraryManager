namespace LibraryManager.Persistence.Configurations
{
    using LibraryManager.Core.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.UseTpcMappingStrategy();

            builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
            builder.Property(x => x.PasswordHash).IsRequired().HasMaxLength(512);
            builder.Property(x => x.LastLogin);

            builder.OwnsOne(x => x.Name, c =>
            {
                c.Property(a => a.FirstName).HasColumnName("FirstName").HasMaxLength(100).IsRequired(true);
                c.Property(a => a.LastName).HasColumnName("LastName").HasMaxLength(100).IsRequired(true);
            });

            builder.Navigation(x => x.Name).IsRequired();
        }
    }
}