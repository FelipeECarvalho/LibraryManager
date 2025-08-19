namespace LibraryManager.Infrastructure.Email
{
    using FluentEmail.Core;
    using LibraryManager.Core.Abstractions;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;

    public sealed class SmtpEmailSender : IEmailSender
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

        public async Task SendAsync(string to, string subject, string body, string cc = null, string bcc = null)
        {
            _logger.LogInformation("Sending an email to {@To} with the subject: {@Subject}", to, subject);

            try
            {
                var emailData = _fluentEmail
                    .To(to)
                    .Body(body)
                    .Subject(subject);

                if (!string.IsNullOrEmpty(cc))
                {
                    emailData.CC(cc);
                }

                if (!string.IsNullOrEmpty(bcc))
                {
                    emailData.BCC(bcc);
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
