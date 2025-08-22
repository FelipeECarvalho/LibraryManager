namespace LibraryManager.Application.Notifications
{
    using LibraryManager.Core.Entities;

    public sealed class LoanCanceledEmail : EmailBase
    {
        private readonly Loan _data;

        public LoanCanceledEmail(Loan data)
            : base(data?.Borrower?.Email)
        {
            ArgumentNullException.ThrowIfNull(data);
            ArgumentNullException.ThrowIfNull(data.Borrower);
            ArgumentNullException.ThrowIfNull(data.Book);

            _data = data;
        }

        public override string Subject => $"Notice: Your loan for the book '{_data.Book.Title}' has been canceled";

        public override string Body => $@"
            Hello, {_data.Borrower.Name.FullName},

            We are informing you that your loan for the book ""{_data.Book.Title}"", which was due on {_data.EndDate:dd/MM/yyyy}, has been canceled because the book was not picked up within the expected timeframe.

            If this was a mistake or you have any questions, please contact our support team.

            Sincerely,  
            The LibraryManager Team";
    }
}