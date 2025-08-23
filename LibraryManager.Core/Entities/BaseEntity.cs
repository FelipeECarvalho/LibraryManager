namespace LibraryManager.Core.Entities
{
    public abstract class BaseEntity
    {
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            CreateDate = DateTimeOffset.UtcNow;
            UpdateDate = DateTimeOffset.UtcNow;
        }

        public Guid Id { get; private set; }

        public DateTimeOffset CreateDate { get; private set; }

        public DateTimeOffset UpdateDate { get; protected set; }

        public bool IsDeleted { get; private set; }

        public void SetDeleted()
        {
            IsDeleted = true;
        }
    }
}
