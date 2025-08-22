namespace LibraryManager.Application.Notifications
{
    using LibraryManager.Core.Entities;

    public sealed class LoanNearOverdueEmail : EmailBase
    {
        private readonly Loan _data;

        public LoanNearOverdueEmail(Loan data)
            : base(data?.Borrower?.Email)
        {
            ArgumentNullException.ThrowIfNull(data);
            ArgumentNullException.ThrowIfNull(data.Borrower);
            ArgumentNullException.ThrowIfNull(data.Book);

            _data = data;
        }

        public override string Subject => $"Attention: Your loan for the book '{_data.Book.Title}' is about to expire!";

        public override string Body => $@"
            Hello, {_data.Borrower.Name.FullName},

            This is a reminder that your loan for the book ""{_data.Book.Title}"" will expire soon.
    
            The return date is: {_data.EndDate:dd/MM/yyyy}.

            Please return it on time to avoid late fees.

            Sincerely,  
            The LibraryManager Team";
    }
}