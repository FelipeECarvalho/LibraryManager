namespace Library.Core.Entities
{
    public class Loan : BaseEntity
    {
        public Loan()
        {
        }

        public Loan(int userId, int bookId, DateTime startDate, DateTime endDate) : base()
        {
            UserId = userId;
            BookId = bookId;
            StartDate = startDate;
            EndDate = endDate;

            Validate();
        }

        public int UserId { get; init; }
        public User User { get; }

        public int BookId { get; init; }
        public Book Book { get; }

        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; private set; }
        public bool IsReturned { get; private set; }

        public void Update(DateTime endDate)
        {
            EndDate = endDate;
            UpdateDate = DateTime.Now;

            Validate();
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

        private void Validate() 
        {
            if (EndDate < DateTime.Now)
                throw new Exception("The loan end date cannot be smaller than today date");
        }
    }
}
