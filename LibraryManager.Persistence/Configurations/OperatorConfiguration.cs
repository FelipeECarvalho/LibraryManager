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

            builder.Property(a => a.Id).ValueGeneratedNever();
            builder.Property(x => x.CreateDate);
            builder.Property(x => x.UpdateDate);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

            builder.Property(x => x.Email).HasMaxLength(50);
            builder.Property(x => x.Permissions);

            builder.OwnsOne(x => x.Name, c =>
            {
                c.Property(a => a.FirstName).HasColumnName("FirstName").HasMaxLength(100).IsRequired(true);
                c.Property(a => a.LastName).HasColumnName("LastName").HasMaxLength(100).IsRequired(true);
            });


            builder.HasIndex(x => x.Email).IsUnique();

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
