namespace LibraryManager.Application.Models
{
    using LibraryManager.Application.Abstractions.Email;
    using LibraryManager.Core.Entities;

    public sealed class QueuedEmail : BaseEntity, IEmail
    {
        [Obsolete("EntityFrameworkCore constructor")]
        private QueuedEmail()
            : base()
        {
        }

        public QueuedEmail(IEmail emailBase)
        {
            To = emailBase.To;
            Cc = emailBase.Cc;
            Bcc = emailBase.Bcc;
            Body = emailBase.Body;
            IsSent = false;
            RetryCount = 0;
            Subject = emailBase.Subject;
            QueuedAt = DateTimeOffset.UtcNow;
        }

        public string To { get; }

        public string Subject { get; }

        public string Body { get; }

        public string Cc { get; }

        public string Bcc { get; }

        public bool IsSent { get; private set; }

        public int RetryCount { get; private set; }

        public string LastError { get; private set; }

        public DateTimeOffset QueuedAt { get; private set; }

        public DateTimeOffset? SentAt { get; private set; }

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
