namespace LibraryManager.Persistence.Configurations
{
    using LibraryManager.Core.Entities;
    using LibraryManager.Persistence.Constants;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal sealed class OperatorConfiguration : IEntityTypeConfiguration<Operator>
    {
        public void Configure(EntityTypeBuilder<Operator> builder)
        {
            builder.ToTable(TableNames.Operators);

            builder.Property(x => x.Permissions).IsRequired();

            builder.HasIndex(x => x.Email).IsUnique();
        }
    }
}
