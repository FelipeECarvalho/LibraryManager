namespace LibraryManager.Persistence.Configurations
{
    using LibraryManager.Core.Entities.Users;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal sealed class OperatorConfiguration : IEntityTypeConfiguration<Operator>
    {
        public void Configure(EntityTypeBuilder<Operator> builder)
        {
            builder.Property(x => x.Permissions).IsRequired();
        }
    }
}
