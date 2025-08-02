namespace LibraryManager.Infrastructure.Email.Emails
{
    using LibraryManager.Core.Entities;

    public sealed class LoanOverdueEmail : EmailBase
    {
        private readonly Loan _data;

        public LoanOverdueEmail(Loan data)
            : base(data?.Borrower?.Email)
        {
            ArgumentNullException.ThrowIfNull(data);
            ArgumentNullException.ThrowIfNull(data.Borrower);
            ArgumentNullException.ThrowIfNull(data.Book);

            _data = data;
        }

        public override string Subject =>
            $"Overdue Notice: Your loan for the book '{_data.Book.Title}' is overdue!";

        public override string Body => $@"
            Hello, {_data.Borrower.Name.FullName},

            This is a reminder that your loan for the book ""{_data.Book.Title}"" is overdue.

            The return date was: {_data.EndDate:dd/MM/yyyy}.

            Please return it as soon as possible to avoid additional late fees or restrictions on future loans.

            Sincerely,  
            LibraryManager";
    }
}