namespace LibraryManager.Core.Entities
{
    public abstract class BaseEntity
    {
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            CreateDate = DateTimeOffset.Now;
            UpdateDate = DateTimeOffset.Now;
        }

        public Guid Id { get; private set; }

        public DateTimeOffset CreateDate { get; private set; }

        public DateTimeOffset UpdateDate { get; private set; }

        public bool IsDeleted { get; private set; }

        public void SetDeleted()
        {
            IsDeleted = true;
        }
    }
}
