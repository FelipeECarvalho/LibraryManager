namespace LibraryManager.Infrastructure.Email
{
    using LibraryManager.Infrastructure.Email.Emails;

    public interface IEmailService
    {
        Task SendAsync(EmailBase email);

        Task EnqueueAsync(EmailBase email);
    }
}
