namespace LibraryManager.Application.Interfaces
{
    using LibraryManager.Application.Notifications;

    public interface IEmailSender
    {
        Task SendAsync(EmailBase email);
    }
}
