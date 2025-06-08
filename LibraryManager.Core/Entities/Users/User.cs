namespace LibraryManager.Core.Entities.Users
{
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

        protected User(Name name, Email email, string passwordHash)
            : base()
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
        }

        public Name Name { get; protected set; }

        public Email Email { get; protected set; }

        public string PasswordHash { get; protected set; }

        public DateTimeOffset? LastLogin { get; protected set; }

        public void UpdateLastLogin()
        {
            LastLogin = DateTimeOffset.UtcNow;
        }
    }
}
