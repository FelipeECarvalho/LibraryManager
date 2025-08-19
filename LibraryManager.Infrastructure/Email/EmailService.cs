namespace LibraryManager.Infrastructure.Email
{
    using LibraryManager.Core.Abstractions;

    internal sealed class EmailService : IEmailService
    {
        private readonly IEmailSender _emailSender;

        public EmailService(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task EnqueueAsync(string to, string subject, string body, string cc = null, string bcc = null)
        {
            await Task.Delay(1);
            throw new NotImplementedException();
        }

        public async Task SendAsync(string to, string subject, string body, string cc = null, string bcc = null)
        {
            await _emailSender.SendAsync(to, subject, body, cc, bcc);
        }
    }
}
