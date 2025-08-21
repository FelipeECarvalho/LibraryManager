namespace LibraryManager.Infrastructure.BackgroundJobs.ScheduledJobs.ProcessCancelLoans
{
    using Microsoft.Extensions.Options;
    using Quartz;

    internal sealed class ProcessCancelLoansJobSetup : IConfigureOptions<QuartzOptions>
    {
        private static readonly JobKey JobKey = JobKey.Create(nameof(ProcessCancelLoansJob));

        public void Configure(QuartzOptions options)
        {
            options
                .AddJob<ProcessCancelLoansJob>(jobBuilder =>
                {
                    jobBuilder.WithIdentity(JobKey);
                    jobBuilder.UsingJobData("RetryCount", 0);
                })
                .AddTrigger(q =>
                {
                    q.ForJob(JobKey);
                    q.WithCronSchedule("0 0 * * * ?");
                });
        }
    }
}
