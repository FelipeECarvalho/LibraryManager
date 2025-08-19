namespace LibraryManager.Infrastructure.Email
{
    using LibraryManager.Infrastructure.Email.Emails;

    public interface IEmailSender
    {
        Task SendAsync(string to, string subject, string body, string cc = null, string bcc = null);
    }
}
