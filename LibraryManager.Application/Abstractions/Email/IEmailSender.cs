namespace LibraryManager.Application.Abstractions.Email
{
    public interface IEmailSender
    {
        Task SendAsync(IEmail email);
    }
}
