namespace LibraryManager.Infrastructure.Email
{
    using FluentEmail.Core;
    using LibraryManager.Application.Interfaces;
    using LibraryManager.Application.Notifications;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;

    public sealed class SmtpEmailSender
        : IEmailSender
    {
        private readonly IFluentEmail _fluentEmail;
        private readonly ILogger<SmtpEmailSender> _logger;

        public SmtpEmailSender(
            IFluentEmail fluentEmail,
            ILogger<SmtpEmailSender> logger)
        {
            _fluentEmail = fluentEmail;
            _logger = logger;
        }

        public async Task SendAsync(EmailBase email)
        {
            _logger.LogInformation("Sending an email to {@To} with the subject: {@Subject}", email.To, email.Subject);

            try
            {
                var emailData = _fluentEmail
                    .To(email.To)
                    .Body(email.Body)
                    .Subject(email.Subject);

                if (!string.IsNullOrEmpty(email.Cc))
                {
                    emailData.CC(email.Cc);
                }

                if (!string.IsNullOrEmpty(email.Bcc))
                {
                    emailData.BCC(email.Bcc);
                }

                await emailData.SendAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error ocurred when sending an email.");
                throw;
            }
        }
    }
}
