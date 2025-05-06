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

        public int Id { get; init; }
        public Guid Guid { get; init; }
        public DateTime CreateDate { get; init; }
        public DateTime UpdateDate { get; protected set; }
        public bool IsDeleted { get; protected set; }

        public void Delete() 
        { 
            IsDeleted = true;
            UpdateDate = DateTime.Now;
        }
    }
}
