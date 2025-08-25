namespace LibraryManager.Core.Entities
{
    public sealed class RefreshToken : BaseEntity
    {

        [Obsolete("EntityFrameworkCore constructor")]
        private RefreshToken()
            : base()
        {
            
        }

        public RefreshToken(User user, string token)
        {
            Token = token;
            User = user;
            ExpiresOn = DateTimeOffset.UtcNow.AddDays(7);
        }

        public string Token { get; private set; }

        public DateTimeOffset ExpiresOn { get; private set; }

        public Guid UserId { get; private set; }

        public User User { get; private set; }

        public bool IsExpired()
        {
            return DateTimeOffset.UtcNow > ExpiresOn;
        }

        public void Update(string token)
        {
            Token = token;
            ExpiresOn = DateTimeOffset.UtcNow.AddDays(7);
        }
    }
}
