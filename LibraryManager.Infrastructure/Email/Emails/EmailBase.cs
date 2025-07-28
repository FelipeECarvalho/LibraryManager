namespace LibraryManager.Infrastructure.Email.Emails
{
    public interface IEmailBase
    {
        string Subject { get; }

        string Body { get; }

        string Cc { get; }

        string Bcc { get; }

        string To { get; }
    }
}
