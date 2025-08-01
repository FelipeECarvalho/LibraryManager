﻿namespace LibraryManager.Persistence.Configurations
{
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Extensions;
    using LibraryManager.Persistence.Constants;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal sealed class LoanConfiguration : BaseEntityConfiguration<Loan>
    {
        public override void Configure(EntityTypeBuilder<Loan> builder)
        {
            base.Configure(builder);

            builder.ToTable(TableNames.Loans);

            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.Observation).HasMaxLength(256).IsRequired(false);
            builder.Property(x => x.TotalOverdueFee).HasPrecision(10, 2).IsRequired(false);

            builder.HasOne(x => x.Borrower)
                .WithMany(x => x.Loans)
                .IsRequired()
                .HasForeignKey(x => x.BorrowerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Book)
                .WithMany(x => x.Loans)
                .IsRequired()
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            var bookUnavailableStatuses = LoanStatusExtensions.BookUnavailable()
                .Select(x => (int)x);

            builder.HasIndex(x => new { x.BorrowerId, x.BookId, x.Status })
                .HasFilter($"[Status] in ({string.Join(",", bookUnavailableStatuses)})")
                .IsUnique();
        }
    }
}
