namespace LibraryManager.Persistence.Configurations
{
    using LibraryManager.Infrastructure.Email.Emails;
    using LibraryManager.Persistence.Constants;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal sealed class QueuedEmailConfiguration : IEntityTypeConfiguration<QueuedEmail>
    {
        public void Configure(EntityTypeBuilder<QueuedEmail> builder)
        {
            builder.ToTable(TableNames.QueuedEmails);

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id).IsRequired().ValueGeneratedNever();
            builder.Property(x => x.QueuedAt).IsRequired();
            builder.Property(x => x.RetryCount).HasDefaultValue(0);
            builder.Property(x => x.LastError).HasMaxLength(2048);
            builder.Property(x => x.SentAt);
            builder.Property(x => x.IsSent).HasDefaultValue(false);

            builder.Property(x => x.Cc).HasMaxLength(256);
            builder.Property(x => x.Bcc).HasMaxLength(256);
            builder.Property(x => x.To).HasMaxLength(256).IsRequired();
            builder.Property(x => x.Subject).HasMaxLength(512).IsRequired();
            builder.Property(x => x.Body).IsRequired();

            builder.HasIndex(x => new { x.IsSent, x.RetryCount })
                   .HasDatabaseName("IX_QueuedEmails_ProcessingStatus");
        }
    }
}
