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

        public Guid UserId { get; private set; }

        public User User { get; private set; }

        public Guid BookId { get; private set; }

        public Book Book { get; private set; }

        public DateTime StartDate { get; private set; }

        public DateTime EndDate { get; private set; }

        public LoanStatus LoanStatus { get; private set; }

        public void Update(DateTime endDate)
        {
            EndDate = endDate;
        }

        public void Create(Guid userId, Guid bookId, DateTime startDate, DateTime endDate)
        {
            UserId = userId;
            BookId = bookId;
            StartDate = startDate;
            EndDate = endDate;
            LoanStatus = LoanStatus.Requested;
        }

        public void Return()
        {
            if (LoanStatus == LoanStatus.Requested)
            {
                return;
            }

            LoanStatus = LoanStatus.Returned;
        }
    }
}
