namespace Library.Core.Entities
{
    public class Loan : BaseEntity
    {
        public int UserId { get; init; }
        public User User { get; }

        public int BookId { get; init; }
        public Book Book { get; }

        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; private set; }
        public bool IsReturned { get; private set; }

        public Loan(int userId, int bookId, DateTime startDate, DateTime endDate) : base() 
        {
            UserId = userId;
            BookId = bookId;
            StartDate = startDate;
            EndDate = endDate;
        }

        public void Update(DateTime endDate)
        {
            EndDate = endDate;
            UpdateDate = DateTime.Now;
        }

        public void Return()
        {
            if (IsReturned)
            {
                return;
            }

            UpdateDate = DateTime.Now;
            IsReturned = true;
        }
    }
}
