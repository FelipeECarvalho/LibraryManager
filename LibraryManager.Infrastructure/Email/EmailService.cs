namespace LibraryManager.Infrastructure.Email
{
    using LibraryManager.Application.Abstractions.Email;
    using LibraryManager.Application.Abstractions.Repositories;
    using LibraryManager.Application.Models;
    using LibraryManager.Infrastructure.BackgroundJobs.QueueWorkers;
    using LibraryManager.Infrastructure.Constants;
    using Quartz;

    internal sealed class EmailService
        : IEmailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IQueuedEmailRepository _queuedEmailRepository;

        public EmailService(
            IUnitOfWork unitOfWork,
            IEmailSender emailSender,
            IQueuedEmailRepository queuedEmailRepository,
            ISchedulerFactory schedulerFactory)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
            _queuedEmailRepository = queuedEmailRepository;
            _schedulerFactory = schedulerFactory;
        }

        public async Task EnqueueAsync(IEmail email)
        {
            var queuedEmail = await CreateQueuedEmailAsync(email);
            await ScheduleEmailJobAsync(queuedEmail);
        }

        public async Task SendAsync(IEmail email)
        {
            await _emailSender.SendAsync(email);
        }

        private async Task ScheduleEmailJobAsync(QueuedEmail queuedEmail)
        {
            var scheduler = await _schedulerFactory.GetScheduler();

            var job = JobBuilder.Create<SendEmailJob>()
                .WithIdentity(string.Format(BackgroundJobConstants.Email.QueuedEmailIdentity, queuedEmail.Id))
                .UsingJobData(BackgroundJobConstants.Email.QueuedEmailIdKey, queuedEmail.Id)
                .Build();

            var trigger = TriggerBuilder.Create()
                .ForJob(job)
                .StartNow()
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        private async Task<QueuedEmail> CreateQueuedEmailAsync(IEmail email)
        {
            var queuedEmail = new QueuedEmail(email);
            _queuedEmailRepository.Add(queuedEmail);

            await _unitOfWork.SaveChangesAsync();

            return queuedEmail;
        }
    }
}
