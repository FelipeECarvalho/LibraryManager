namespace LibraryManager.Core.Entities
{
    using LibraryManager.Core.Abstractions;
    using LibraryManager.Core.ValueObjects;

    /// <summary>
    /// Base user, responsible for storing common user's data
    /// </summary>
    public abstract class User : BaseEntity
    {
        [Obsolete("EntityFrameworkCore constructor", true)]
        protected User()
        {
        }

        protected User(Name name, Email email, string passwordHash, Guid libraryId)
            : base()
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            LibraryId = libraryId;
        }

        public Name Name { get; protected set; }

        public Email Email { get; protected set; }

        public string PasswordHash { get; protected set; }

        public DateTimeOffset? LastLogin { get; protected set; }

        public Guid LibraryId { get; protected set; }

        public Library Library { get; protected set; }

        public void UpdateLastLogin()
        {
            LastLogin = DateTimeOffset.UtcNow;
        }

        public bool VerifyPassword(string password, IPasswordHasher passwordHasher)
        {
            return passwordHasher.Verify(password, PasswordHash);
        }
    }
}
