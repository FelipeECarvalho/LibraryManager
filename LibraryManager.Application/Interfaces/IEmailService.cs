namespace LibraryManager.Application.Interfaces
{
    using LibraryManager.Application.Notifications;

    public interface IEmailService
    {
        Task SendAsync(EmailBase email);

        Task EnqueueAsync(EmailBase email);
    }
}
