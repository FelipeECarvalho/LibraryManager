namespace LibraryManager.Application.Abstractions.Email
{
    using LibraryManager.Application.Notifications;

    public interface IEmailSender
    {
        Task SendAsync(EmailBase email);
    }
}
