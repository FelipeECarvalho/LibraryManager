namespace LibraryManager.Infrastructure.Email
{
    using LibraryManager.Infrastructure.Email.Emails;

    public interface IEmailSender
    {
        Task SendAsync(EmailBase email); 
    }
}
