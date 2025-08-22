namespace LibraryManager.Application.Models
{
    using LibraryManager.Core.Entities;

    public sealed class RefreshToken : BaseEntity
    {
        public string Token { get; set; }

        public DateTimeOffset ExpiresOn { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
