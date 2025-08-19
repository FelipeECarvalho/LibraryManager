namespace LibraryManager.Core.Abstractions
{
    public interface IEmailSender
    {
        Task SendAsync(string to, string subject, string body, string cc = null, string bcc = null);
    }
}
