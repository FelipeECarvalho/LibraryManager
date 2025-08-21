namespace LibraryManager.Application.Abstractions.Email
{
    public interface IEmailService
    {
        Task SendAsync(IEmail email);

        Task EnqueueAsync(IEmail email);
    }
}
