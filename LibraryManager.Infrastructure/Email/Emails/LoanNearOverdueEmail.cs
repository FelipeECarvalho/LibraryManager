namespace LibraryManager.Infrastructure.Email.Emails
{
    public sealed class LoanNearOverdueEmail : IEmailBase
    {
        public string Subject => "Your loan is near overdue!";

        public string Body => "Your loan is near overdue! Return it or ";

        public string Cc { get; set; }

        public string Bcc { get; set; }

        public string To { get; set; }
    }
}
