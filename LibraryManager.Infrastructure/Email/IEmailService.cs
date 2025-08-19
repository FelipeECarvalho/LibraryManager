namespace LibraryManager.Infrastructure.Email
{
    using LibraryManager.Infrastructure.Email.Emails;

    public interface IEmailService
    {
        Task SendAsync(string to, string subject, string body, string cc = null, string bcc = null);

        Task EnqueueAsync(string to, string subject, string body, string cc = null, string bcc = null);
    }
}
