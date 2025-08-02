namespace LibraryManager.Infrastructure.BackgroundJobs.ProcessNearOverdueLoanStatus
{
    using Microsoft.Extensions.Options;
    using Quartz;

    internal sealed class ProcessNearOverdueLoanStatusJobSetup : IConfigureOptions<QuartzOptions>
    {
        private static readonly JobKey JobKey = JobKey.Create(nameof(ProcessNearOverdueLoanStatusJob));

        public void Configure(QuartzOptions options)
        {
            options
                .AddJob<ProcessNearOverdueLoanStatusJob>(jobBuilder => jobBuilder.WithIdentity(JobKey))
                .AddTrigger(q =>
                {
                    q.ForJob(JobKey);
                    q.WithCronSchedule("0 0 * * * ?");
                });
        }
    }
}
