namespace Library.Core.Entities
{
    public class Loan : BaseEntity
    {
        [Obsolete("EntityFrameworkCore constructor")]
        private Loan() : base()
        {
        }

        public Loan(int userId, int bookId, DateTime startDate, DateTime endDate)
            : base()
        {
            UserId = userId;
            BookId = bookId;
            StartDate = startDate;
            EndDate = endDate;
        }

        public int UserId { get; private set; }

        public User User { get; private set; }

        public int BookId { get; private set; }

        public Book Book { get; private set; }

        public DateTime StartDate { get; private set; }

        public DateTime EndDate { get; private set; }

        public bool IsReturned { get; private set; }

        public void Update(DateTime endDate)
        {
            EndDate = endDate;
        }

        public void Return()
        {
            if (IsReturned)
            {
                return;
            }

            IsReturned = true;
        }
    }
}
