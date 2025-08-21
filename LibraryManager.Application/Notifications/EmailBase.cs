namespace LibraryManager.Application.Notifications
{
    using LibraryManager.Application.Abstractions.Email;

    public abstract class EmailBase(
        string to,
        string cc = null,
        string bcc = null)
        : IEmail
    {
        public string To { get; } = to;
        public string Cc { get; } = cc;
        public string Bcc { get; } = bcc;
        public virtual string Body { get; }
        public virtual string Subject { get; }
    }
}
