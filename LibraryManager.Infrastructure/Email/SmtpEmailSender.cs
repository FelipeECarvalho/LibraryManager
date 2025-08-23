namespace LibraryManager.Infrastructure.Email
{
    using FluentEmail.Core;
    using LibraryManager.Application.Abstractions;
    using LibraryManager.Application.Abstractions.Email;
    using System.Threading.Tasks;

    public sealed class SmtpEmailSender
        : IEmailSender
    {
        private readonly IFluentEmail _fluentEmail;
        private readonly IAppLogger<SmtpEmailSender> _logger;

        public SmtpEmailSender(
            IFluentEmail fluentEmail,
            IAppLogger<SmtpEmailSender> logger)
        {
            _fluentEmail = fluentEmail;
            _logger = logger;
        }

        public async Task SendAsync(IEmail email)
        {
            _logger.LogInformation("Sending an email to {@To} with the subject: {@Subject}. Date {DatetimeNow}", email.To, email.Subject, DateTimeOffset.UtcNow);

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

                _logger.LogInformation("Email sent successfully. Date: {DatetimeNow}", DateTimeOffset.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error ocurred when sending an email.");
                throw;
            }
        }
    }
}
