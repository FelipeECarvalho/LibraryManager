namespace LibraryManager.Infrastructure.Email.Emails
{
    public abstract class EmailBase
    {
        public string To { get; private set; }

        public string Subject { get; private set; }

        public string Body { get; private set; }

        public string Cc { get; private set; }

        public string Bcc { get; private set; }
    }
}
