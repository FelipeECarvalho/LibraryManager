namespace LibraryManager.Infrastructure.BackgroundJobs.QueueWorkers
{
    using LibraryManager.Application.Abstractions;
    using LibraryManager.Application.Abstractions.Email;
    using LibraryManager.Application.Abstractions.Repositories;
    using LibraryManager.Application.Models;
    using Quartz;
    using System.Threading.Tasks;

    [DisallowConcurrentExecution]
    [PersistJobDataAfterExecution]
    internal sealed class SendEmailJob : IJob
    {
        private const int MaxRetries = 3;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppLogger<SendEmailJob> _logger;
        private readonly IEmailService _emailService;
        private readonly IQueuedEmailRepository _queuedEmailRepository;

        public SendEmailJob(
            IUnitOfWork unitOfWork,
            IAppLogger<SendEmailJob> logger,
            IEmailService emailService,
            IQueuedEmailRepository queuedEmailRepository)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _emailService = emailService;
            _queuedEmailRepository = queuedEmailRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var jobDataMap = context.MergedJobDataMap;
            var queuedEmailId = jobDataMap.GetGuid("QueuedEmailId");

            var queuedEmail = await _queuedEmailRepository
                .GetByIdAsync(queuedEmailId);

            if (queuedEmail is null || queuedEmail.IsSent)
            {
                _logger.LogInformation("Queued email with id {QueuedEmailId} already sent, exiting SendEmailJob", queuedEmailId);
                return;
            }

            try
            {
                await _emailService.SendAsync(queuedEmail);
                queuedEmail.MarkAsSent();
            }
            catch (Exception ex)
            {
                queuedEmail.MarkAsFailed(ex.Message);
                
                _logger.LogError(ex, "Error when sending email {EmailId}. Retry {RetryCount} of {MaxRetries}", queuedEmail.Id, queuedEmail.RetryCount, MaxRetries);

                if (queuedEmail.RetryCount >= MaxRetries)
                {
                    _logger.LogCritical("E-mail {EmailId} has reach the max number of retries - marked as a permanent error.", queuedEmail.Id);
                }
                else
                {
                    await RescheduleEmailJobAndLogAsync(queuedEmail, context);
                }
            }
            finally
            {
                await _unitOfWork.SaveChangesAsync();
            }
        }

        private async Task RescheduleEmailJobAndLogAsync(
            QueuedEmail queuedEmail,
            IJobExecutionContext context)
        {
            var delayInSeconds = Math.Pow(2, queuedEmail.RetryCount) * 60;

            var newTrigger = TriggerBuilder.Create()
                .ForJob(context.JobDetail.Key)
                .StartAt(DateTimeOffset.UtcNow.AddSeconds(delayInSeconds))
                .Build();

            await context.Scheduler.ScheduleJob(newTrigger);

            _logger.LogWarning("E-mail {EmailId} will be resent in {Delay} seconds.", queuedEmail.Id, delayInSeconds);
        }
    }
}
