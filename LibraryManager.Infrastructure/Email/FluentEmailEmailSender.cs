namespace LibraryManager.Infrastructure.Email
{
    using FluentEmail.Core;
    using LibraryManager.Infrastructure.Email.Emails;
    using System.Threading.Tasks;

    public sealed class FluentEmailEmailSender : IEmailSender
    {
        private readonly IFluentEmail _fluentEmail;

        public FluentEmailEmailSender(IFluentEmail fluentEmail) 
        {
            _fluentEmail = fluentEmail;
        }

        public async Task SendAsync(EmailBase email)
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
    }
}
