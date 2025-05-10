namespace Library.Core.Entities
{
    public abstract class BaseEntity
    {
        protected BaseEntity()
        {
            Guid = Guid.NewGuid();
            CreateDate = DateTime.Now;
            UpdateDate = DateTime.Now;
        }

        public int Id { get; private set; }

        public Guid Guid { get; private set; }

        public DateTime CreateDate { get; private set; }

        public DateTime UpdateDate { get; private set; }

        public bool IsDeleted { get; private set; }

        public void SetDeleted()
        {
            IsDeleted = true;
        }
    }
}
