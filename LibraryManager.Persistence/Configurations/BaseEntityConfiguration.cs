namespace LibraryManager.Persistence.Configurations
{
    using LibraryManager.Core.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class BaseEntityConfiguration : IEntityTypeConfiguration<BaseEntity>
    {
        public void Configure(EntityTypeBuilder<BaseEntity> builder)
        {
            builder.UseTpcMappingStrategy();

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id).IsRequired().ValueGeneratedNever();
            builder.Property(x => x.CreateDate).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired().HasDefaultValue(false);

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
