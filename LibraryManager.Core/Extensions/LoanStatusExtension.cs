namespace LibraryManager.Core.Extensions
{
    using LibraryManager.Core.Enums;

    public static class LoanStatusExtension
    {
        public static IList<LoanStatus> BookUnavailable()
        {
            return
            [
                LoanStatus.Requested,
                LoanStatus.Approved,
                LoanStatus.Borrowed,
                LoanStatus.Overdue
            ];
        }

        public static IList<LoanStatus> BookInUserHands()
        {
            return
            [
                LoanStatus.Borrowed,
                LoanStatus.Overdue
            ];
        }

        public static bool CanBeCancelled(this LoanStatus status)
        {
            return new[]
            {
                LoanStatus.Requested,
                LoanStatus.Approved
            }.Contains(status);
        }

        public static bool IsWithUser(this LoanStatus status)
            => BookInUserHands().Contains(status);

        public static bool IsBookUnavailable(this LoanStatus status)
            => BookUnavailable().Contains(status);

        public static bool CanApproveFrom(this LoanStatus newStatus, LoanStatus currentStatus)
            => newStatus == LoanStatus.Approved && currentStatus == LoanStatus.Requested;

        public static bool CanCancelFrom(this LoanStatus newStatus, LoanStatus currentStatus)
            => newStatus == LoanStatus.Cancelled && currentStatus.CanBeCancelled();

        public static bool CanBorrowFrom(this LoanStatus newStatus, LoanStatus currentStatus)
            => newStatus == LoanStatus.Borrowed && currentStatus == LoanStatus.Approved;

        public static bool CanReturnFrom(this LoanStatus newStatus, LoanStatus currentStatus)
            => newStatus == LoanStatus.Returned && currentStatus.IsWithUser();
    }
}
