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

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id).IsRequired().ValueGeneratedNever();
            builder.Property(x => x.CreateDate).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired().HasDefaultValue(false);

            builder.Property(x => x.Permissions).IsRequired();

            builder.HasIndex(x => x.Email).IsUnique();

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
