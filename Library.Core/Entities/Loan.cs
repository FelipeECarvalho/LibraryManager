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
            LoanStatus = LoanStatus.Requested;
        }

        public Guid UserId { get; private set; }

        public User User { get; private set; }

        public Guid BookId { get; private set; }

        public Book Book { get; private set; }

        public DateTimeOffset StartDate { get; private set; }

        public DateTimeOffset EndDate { get; private set; }

        public LoanStatus LoanStatus { get; private set; }

        public void Update(DateTimeOffset endDate)
        {
            EndDate = endDate;
        }

        public void Return()
        {
        }
    }
}
