namespace LibraryManager.Infrastructure.BackgroundJobs.ScheduledJobs.ProcessNearOverdueLoans
{
    using Microsoft.Extensions.Options;
    using Quartz;

    internal sealed class ProcessNearOverdueLoansJobSetup : IConfigureOptions<QuartzOptions>
    {
        private static readonly JobKey JobKey = JobKey.Create(nameof(ProcessNearOverdueLoansJob));

        public void Configure(QuartzOptions options)
        {
            options
                .AddJob<ProcessNearOverdueLoansJob>(jobBuilder => jobBuilder.WithIdentity(JobKey))
                .AddTrigger(q =>
                {
                    q.ForJob(JobKey);
                    q.WithCronSchedule("0 0 * * * ?");
                });
        }
    }
}
