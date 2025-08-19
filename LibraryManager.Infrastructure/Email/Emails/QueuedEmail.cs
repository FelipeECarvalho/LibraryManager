namespace LibraryManager.Infrastructure.Email.Emails
{
    public class QueuedEmail : EmailBase
    {
        public Guid Id { get; private set; }

        public DateTimeOffset QueuedAt { get; private set; }

        public DateTimeOffset? SentAt { get; private set; }

        public bool IsSent { get; private set; }

        public int RetryCount { get; private set; }

        public string LastError { get; private set; }

        public QueuedEmail(
            string to,
            string subject,
            string body,
            string cc = null,
            string bcc = null)
            : base(to, cc, bcc)
        {
            To = to;
            Cc = cc;
            Bcc = bcc;
            Body = body;
            IsSent = false;
            RetryCount = 0;
            Subject = subject;
            Id = Guid.NewGuid();
            QueuedAt = DateTimeOffset.UtcNow;
        }

        public void MarkAsSent()
        {
            IsSent = true;
            LastError = null;
            SentAt = DateTimeOffset.UtcNow;
        }

        public void MarkAsFailed(string errorMessage)
        {
            RetryCount++;
            IsSent = false;
            LastError = errorMessage;
        }
    }
}
