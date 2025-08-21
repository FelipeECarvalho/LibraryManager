namespace LibraryManager.Application.Abstractions.Email
{
    using LibraryManager.Application.Notifications;

    public interface IEmailService
    {
        Task SendAsync(EmailBase email);

        Task EnqueueAsync(EmailBase email);
    }
}
