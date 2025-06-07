namespace LibraryManager.Core.Entities.Users
{
    using LibraryManager.Core.ValueObjects;

    /// <summary>
    /// Base user, responsible for storing common user's data
    /// </summary>
    public abstract class User : BaseEntity
    {
        public Name Name { get; protected set; }

        public string Email { get; protected set; }

        public string PasswordHash { get; protected set; }

        public DateTimeOffset? LastLogin { get; protected set; }
    }
}
