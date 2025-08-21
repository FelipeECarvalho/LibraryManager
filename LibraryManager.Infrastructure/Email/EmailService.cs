namespace LibraryManager.Infrastructure.Email
{
    using LibraryManager.Application.Abstractions.Email;
    using LibraryManager.Application.Abstractions.Repositories;
    using LibraryManager.Application.Models;

    internal sealed class EmailService 
        : IEmailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        private readonly IQueuedEmailRepository _queuedEmailRepository;

        public EmailService(
            IUnitOfWork unitOfWork,
            IEmailSender emailSender,
            IQueuedEmailRepository queuedEmailRepository)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
            _queuedEmailRepository = queuedEmailRepository;
        }

        public async Task EnqueueAsync(IEmail email)
        {
            var queuedEmail = new QueuedEmail(email);

            _queuedEmailRepository.Add(queuedEmail);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task SendAsync(IEmail email)
        {
            await _emailSender.SendAsync(email);
        }
    }
}
