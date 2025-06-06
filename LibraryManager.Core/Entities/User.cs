namespace LibraryManager.Core.Entities
{
    using LibraryManager.Core.ValueObjects;

    public abstract class User : BaseEntity
    {
        public Name Name { get; protected set; }

        public string Email { get; protected set; }

        public string PasswordHash { get; protected set; }

        public DateTimeOffset? LastLogin { get; protected set; }
    }
}
