namespace LibraryManager.Infrastructure.Email
{
    using LibraryManager.Infrastructure.Email.Emails;
    using System;

    internal sealed class EmailService : IEmailService
    {
        private readonly IEmailSender _emailSender;

        public EmailService(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task EnqueueAsync(EmailBase email)
        {
            throw new NotImplementedException();
        }

        public async Task SendAsync(EmailBase email)
        {
            await _emailSender.SendAsync(email);
        }
    }
}
