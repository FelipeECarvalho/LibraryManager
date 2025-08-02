namespace LibraryManager.Infrastructure.Email.Emails
{
    public abstract class EmailBase
    {
        public string To { get; protected set; }
        public string Cc { get; protected set; }
        public string Bcc { get; protected set; }
        public virtual string Body { get; protected set; }
        public virtual string Subject { get; protected set; }

        protected EmailBase(string to, string cc = null, string bcc = null)
        {
            To = to;
            Cc = cc;
            Bcc = bcc;
        }
    }
}
