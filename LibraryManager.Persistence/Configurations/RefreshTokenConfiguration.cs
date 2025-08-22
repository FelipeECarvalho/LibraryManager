namespace LibraryManager.Persistence.Configurations
{
    using LibraryManager.Application.Models;
    using LibraryManager.Persistence.Constants;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal sealed class RefreshTokenConfiguration 
        : BaseEntityConfiguration<RefreshToken>
    {
        public override void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            base.Configure(builder);

            builder.ToTable(TableNames.RefreshTokens);

            builder.Property(x => x.Token).HasMaxLength(200).IsRequired(true);
            builder.Property(x => x.ExpiresOn);

            builder.HasIndex(x => x.Token).IsUnique();

            builder
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);
        }
    }
}
