namespace LibraryManager.Infrastructure.BackgroundJobs
{
    using LibraryManager.Application.Abstractions;
    using LibraryManager.Infrastructure.Constants;
    using Microsoft.Extensions.Options;
    using Quartz;
    using System;
    using System.Threading.Tasks;

    [DisallowConcurrentExecution]
    [PersistJobDataAfterExecution]
    internal abstract class ResilientJob<T> : IJob
    {
        protected readonly IAppLogger<T> _logger;
        protected readonly BackgroundJobOptions _jobSettings;

        protected ResilientJob(
            IAppLogger<T> logger,
            IOptions<BackgroundJobOptions> jobSettings)
        {
            _logger = logger;
            _jobSettings = jobSettings.Value;
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

                context.MergedJobDataMap.TryGetIntValue(BackgroundJobConstants.Default.RetryCountKey, out var retryCount);

                if (retryCount >= _jobSettings.DefaultMaxRetries)
                {
                    _logger.LogCritical("Job {JobName} reached the limit of {MaxRetries} tries.", jobName, _jobSettings.DefaultMaxRetries);
                    return;
                }

                context.MergedJobDataMap.Put(BackgroundJobConstants.Default.RetryCountKey, retryCount + 1);

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
