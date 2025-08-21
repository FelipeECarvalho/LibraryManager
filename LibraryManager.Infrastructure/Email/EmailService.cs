namespace LibraryManager.Infrastructure.Email
{
    using LibraryManager.Application.Abstractions.Email;
    using LibraryManager.Application.Notifications;

    internal sealed class EmailService : IEmailService
    {
        private readonly IEmailSender _emailSender;

        public EmailService(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task EnqueueAsync(EmailBase email)
        {
            await Task.Delay(1);
            throw new NotImplementedException();
        }

        public async Task SendAsync(EmailBase email)
        {
            await _emailSender.SendAsync(email);
        }
    }
}
