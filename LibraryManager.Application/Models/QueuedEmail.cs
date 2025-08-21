namespace LibraryManager.Application.Models
{
    using LibraryManager.Application.Interfaces;
    using LibraryManager.Core.Entities;

    public sealed class QueuedEmail : BaseEntity, IEmail
    {
        public DateTimeOffset QueuedAt { get; private set; }

        public DateTimeOffset? SentAt { get; private set; }

        public string To { get; }

        public string Subject { get; }

        public string Body { get; }

        public string Cc { get; }

        public string Bcc { get; }

        public bool IsSent { get; private set; }

        public int RetryCount { get; private set; }

        public string LastError { get; private set; }

        public QueuedEmail(
            string to,
            string subject,
            string body,
            string cc = null,
            string bcc = null)
        {
            To = to;
            Cc = cc;
            Bcc = bcc;
            Body = body;
            IsSent = false;
            RetryCount = 0;
            Subject = subject;
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
