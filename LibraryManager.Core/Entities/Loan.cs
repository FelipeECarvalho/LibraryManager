namespace LibraryManager.Core.Entities
{
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Enums;
    using LibraryManager.Core.Errors;
    using LibraryManager.Core.Extensions;

    public class Loan : BaseEntity
    {
        // TODO: Add library configuration
        private const int DaysToCancelation = 7;

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

            UpdateDate = DateTimeOffset.Now;

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
            UpdateDate = DateTimeOffset.Now;

            return Result.Success();
        }

        public bool CanBeCanceled()
        {
            return Status == LoanStatus.Approved
                && DateTimeOffset.UtcNow > StartDate.AddDays(DaysToCancelation);
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
