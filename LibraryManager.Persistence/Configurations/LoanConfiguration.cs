namespace LibraryManager.Persistence.Configurations
{
    using LibraryManager.Core.Entities;
    using LibraryManager.Persistence.Constants;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal sealed class LoanConfiguration : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.ToTable(TableNames.Loans);

            builder.HasKey(x => x.Id);

            builder.Property(a => a.Id).ValueGeneratedNever();
            builder.Property(x => x.CreateDate);
            builder.Property(x => x.UpdateDate);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

            builder.Property(x => x.StartDate);
            builder.Property(x => x.EndDate);
            builder.Property(x => x.Status);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Loans)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Book)
                .WithMany(x => x.Loans)
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => new { x.UserId, x.BookId, x.Status })
                .IsUnique()
                .HasFilter("Status in (0, 1, 2, 4)");

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
