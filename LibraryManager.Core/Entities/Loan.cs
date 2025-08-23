namespace LibraryManager.Core.Entities
{
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Enums;
    using LibraryManager.Core.Errors;
    using LibraryManager.Core.Extensions;

    public class Loan : BaseEntity
    {
        // TODO: Add library configuration
        private const int MaxDaysAfterApprovalToCancel = 7;
        private const int DaysBeforeDueToMarkAsNearOverdue = 1;
        private const decimal OverdueFee = 5m;
        private const decimal DailyOverdueFee = 1m;

        [Obsolete("EntityFrameworkCore constructor")]
        private Loan()
            : base()
        {
        }

        public Loan(
            Guid borrowerId,
            Guid bookId,
            DateTimeOffset startDate,
            DateTimeOffset endDate,
            string observation)
            : base()
        {
            BorrowerId = borrowerId;
            BookId = bookId;
            StartDate = startDate;
            EndDate = endDate;
            Status = LoanStatus.Requested;
            Observation = observation;
        }

        public Guid BorrowerId { get; private set; }

        public Borrower Borrower { get; private set; }

        public Guid BookId { get; private set; }

        public Book Book { get; private set; }

        public string Observation { get; set; }

        public DateTimeOffset StartDate { get; private set; }

        public DateTimeOffset EndDate { get; private set; }

        public LoanStatus Status { get; private set; }

        public decimal? TotalOverdueFee { get; private set; }

        public Result Update(DateTimeOffset endDate)
        {
            if (StartDate > endDate)
            {
                return Result.Failure(DomainErrors.Loan.InvalidStartDate);
            }

            EndDate = endDate;

            if (Status == LoanStatus.Overdue &&
                EndDate > DateTimeOffset.UtcNow)
            {
                Status = LoanStatus.Borrowed;
            }

            UpdateDate = DateTimeOffset.UtcNow;

            return Result.Success();
        }

        public Result UpdateStatus(LoanStatus status)
        {
            var validationResult = ValidateStatusTransition(status);

            if (validationResult.IsFailure)
            {
                return validationResult;
            }

            Status = status;
            UpdateDate = DateTimeOffset.UtcNow;

            return Result.Success();
        }

        public void UpdateOverdueFee()
        {
            if (Status != LoanStatus.Overdue)
            {
                return;
            }

            var overdueDays = CalculateOverdueDays();

            if (overdueDays <= 0)
            {
                return;
            }

            TotalOverdueFee = OverdueFee + (overdueDays * DailyOverdueFee);
        }

        public int CalculateOverdueDays()
        {
            return Math.Max(0, (DateTimeOffset.UtcNow.Date - EndDate.Date).Days);
        }

        /// <summary>
        /// In case the user hasn't picked up the book.
        /// </summary>
        public bool CanBeCanceled()
        {
            return Status == LoanStatus.Approved
                && DateTimeOffset.UtcNow > StartDate.AddDays(MaxDaysAfterApprovalToCancel);
        }

        public bool IsNearOverdue()
        {
            return Status == LoanStatus.Borrowed
                && (EndDate.Date - DateTimeOffset.UtcNow.Date).TotalDays == DaysBeforeDueToMarkAsNearOverdue
                && !(DateTimeOffset.UtcNow > EndDate);
        }

        public bool CanBeOverdue()
        {
            return Status == LoanStatus.Borrowed
                && DateTimeOffset.UtcNow > EndDate;
        }

        private Result ValidateStatusTransition(LoanStatus newStatus)
        {
            return newStatus switch
            {
                LoanStatus.Approved when !newStatus.CanApproveFrom(Status) =>
                    Result.Failure(DomainErrors.Loan.CannotApproveWhenNotRequested),

                LoanStatus.Cancelled when !newStatus.CanCancelFrom(Status) =>
                    Result.Failure(DomainErrors.Loan.CannotCancelInThisStatus),

                LoanStatus.Borrowed when !newStatus.CanBorrowFrom(Status) =>
                    Result.Failure(DomainErrors.Loan.CannotBorrowWhenNotApproved),

                LoanStatus.Returned when !newStatus.CanReturnFrom(Status) =>
                    Result.Failure(DomainErrors.Loan.CannotReturnWhenNotBorrowed),

                _ => Result.Success()
            };
        }
    }
}
