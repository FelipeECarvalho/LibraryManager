namespace LibraryManager.Infrastructure.BackgroundJobs
{
    using LibraryManager.Application.Abstractions;
    using Quartz;
    using System;
    using System.Threading.Tasks;

    [DisallowConcurrentExecution]
    [PersistJobDataAfterExecution]
    internal abstract class ResilientJob<T> : IJob
    {
        private const int MaxRetries = 3;
        private const string RetryCountKey = "RetryCount";

        protected readonly IAppLogger<T> _logger;

        protected ResilientJob(IAppLogger<T> logger)
        {
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var jobName = GetType().Name;

            try
            {
                _logger.LogInformation("Starting job {JobName}", jobName);

                await ExecuteCore(context);

                _logger.LogInformation("Completed job {JobName} sucessfully", jobName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when processing job {JobName}", jobName);

                var retryCount = context.MergedJobDataMap.GetIntValue(RetryCountKey);

                if (retryCount >= MaxRetries)
                {
                    _logger.LogCritical("Job {JobName} reached the limit of {MaxRetries} tries.", jobName, MaxRetries);
                    return;
                }

                context.MergedJobDataMap.Put(RetryCountKey, retryCount + 1);

                var jobException = new JobExecutionException(ex)
                {
                    RefireImmediately = true
                };

                throw jobException;
            }
        }

        public abstract Task ExecuteCore(IJobExecutionContext context);
    }
}
