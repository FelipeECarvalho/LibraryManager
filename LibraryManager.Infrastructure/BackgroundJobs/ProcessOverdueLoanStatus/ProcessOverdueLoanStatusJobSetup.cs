namespace LibraryManager.Infrastructure.BackgroundJobs.ProcessOverdueLoanStatus
{
    using Microsoft.Extensions.Options;
    using Quartz;

    internal sealed class ProcessOverdueLoanStatusJobSetup : IConfigureOptions<QuartzOptions>
    {
        public void Configure(QuartzOptions options)
        {
            var jobKey = JobKey.Create(nameof(ProcessOverdueLoanStatusJob));

            options
                .AddJob<ProcessOverdueLoanStatusJob>(jobBuilder => jobBuilder.WithIdentity(jobKey))
                .AddTrigger(q =>
                {
                    q.ForJob(jobKey);
                    q.WithCronSchedule("0 0 * * * ?");
                });
        }
    }
}
