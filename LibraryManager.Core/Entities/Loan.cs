namespace LibraryManager.Core.Entities
{
    using LibraryManager.Core.Enums;
    public class Loan : BaseEntity
    {
        [Obsolete("EntityFrameworkCore constructor")]
        private Loan()
            : base()
        {
        }

        public Loan(Guid userId, Guid bookId, DateTimeOffset startDate, DateTimeOffset endDate)
            : base()
        {
            UserId = userId;
            BookId = bookId;
            StartDate = startDate;
            EndDate = endDate;
            Status = LoanStatus.Requested;
        }

        public Guid UserId { get; private set; }

        public User User { get; private set; }

        public Guid BookId { get; private set; }

        public Book Book { get; private set; }

        public DateTimeOffset StartDate { get; private set; }

        public DateTimeOffset EndDate { get; private set; }

        public LoanStatus Status { get; private set; }

        public void Update(DateTimeOffset endDate)
        {
            EndDate = endDate;

            if (Status == LoanStatus.Overdue &&
                EndDate > DateTimeOffset.UtcNow) 
            {
                Status = LoanStatus.Borrowed;
            }

            UpdateDate = DateTimeOffset.Now;
        }

        public void Return()
        {
            Status = LoanStatus.Returned;
            UpdateDate = DateTimeOffset.Now;
        }
    }
}
