namespace LibraryManager.Core.Enums
{
    using System.ComponentModel;

    public enum LoanStatus
    {
        [Description("The user has requested the loan, pending approval.")]
        Requested,

        [Description("The loan has been approved, waiting for pickup.")]
        Approved,

        [Description("The book has been borrowed by the user.")]
        Borrowed,

        [Description("The book has been returned.")]
        Returned,

        [Description("The return deadline has passed.")]
        Overdue,

        [Description("The loan was cancelled before the book was picked up.")]
        Cancelled
    }

    public static class LoanStatusExtension 
    {
        public static IList<LoanStatus> Active()
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

        public static bool IsWithUser(this LoanStatus status)
            => BookInUserHands().Contains(status);

        public static bool IsActive(this LoanStatus status) 
            => Active().Contains(status);
    }
}
