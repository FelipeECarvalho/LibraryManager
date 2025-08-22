namespace LibraryManager.Application.Notifications
{
    using LibraryManager.Core.Entities;

    public sealed class LoanConfirmationEmail : EmailBase
    {
        private readonly Loan _data;

        public LoanConfirmationEmail(Loan data)
            : base(data?.Borrower?.Email)
        {
            ArgumentNullException.ThrowIfNull(data);
            ArgumentNullException.ThrowIfNull(data.Borrower);
            ArgumentNullException.ThrowIfNull(data.Book);

            _data = data;
        }

        public override string Subject =>
            $"Loan Confirmation: '{_data.Book.Title}'";

        public override string Body => $@"
            Hello, {_data.Borrower.Name.FullName},

            Thank you for borrowing with us! This email confirms your new loan for the book ""{_data.Book.Title}"".

            Here are the details for your reference:
            - Loan Date: {_data.StartDate:dd/MM/yyyy}
            - Return Date: {_data.EndDate:dd/MM/yyyy}

            We hope you enjoy your reading! Please be mindful of the return date to avoid any late fees.

            Best regards,
            The LibraryManager Team";
    }
}