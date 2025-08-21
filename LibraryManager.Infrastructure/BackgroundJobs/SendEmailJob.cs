namespace LibraryManager.Infrastructure.BackgroundJobs
{
    using LibraryManager.Application.Abstractions;
    using LibraryManager.Application.Abstractions.Email;
    using LibraryManager.Application.Abstractions.Repositories;
    using Quartz;
    using System.Threading.Tasks;

    [DisallowConcurrentExecution]
    [PersistJobDataAfterExecution]
    internal sealed class SendEmailJob : IJob
    {
        private const int MaxRetries = 3;
        private const string RetryCountKey = "RetryCount";

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
            var stringQueuedEmailId = jobDataMap.GetString("QueuedEmailId");

            if (!Guid.TryParse(stringQueuedEmailId, out var queuedEmailId))
            {
                _logger.LogError("Error when processing SendEmailJob: failed to convert queued email id {id}", stringQueuedEmailId);
                return;
            }

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
                var retryCount = jobDataMap.GetIntValue(RetryCountKey);

                _logger.LogError(ex, "Error when sending email {EmailId}. Retry {RetryCount} of {MaxRetries}", queuedEmail.Id, retryCount + 1, MaxRetries);

                jobDataMap.Put(RetryCountKey, retryCount + 1);

                if (retryCount + 1 >= MaxRetries)
                {
                    _logger.LogCritical("E-mail {EmailId} has reach the max number of retries - marked as a permanent error.", queuedEmail.Id);
                }
                else
                {
                    var delayInSeconds = Math.Pow(2, retryCount) * 60;

                    var newTrigger = TriggerBuilder.Create()
                        .ForJob(context.JobDetail.Key)
                        .StartAt(DateTimeOffset.UtcNow.AddSeconds(delayInSeconds))
                        .Build();

                    await context.Scheduler.ScheduleJob(newTrigger);

                    _logger.LogWarning("E-mail {EmailId} will be resent in {Delay} seconds.", queuedEmail.Id, delayInSeconds);
                }

                queuedEmail.MarkAsFailed(ex.Message);
            }
            finally
            {
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
