namespace LibraryManager.Infrastructure.BackgroundJobs.ProcessOverdueLoansFee
{
    using Microsoft.Extensions.Options;
    using Quartz;

    internal sealed class ProcessOverdueLoansFeeJobSetup : IConfigureOptions<QuartzOptions>
    {
        private static readonly JobKey JobKey = JobKey.Create(nameof(ProcessOverdueLoansFeeJob));

        public void Configure(QuartzOptions options)
        {
            options
                .AddJob<ProcessOverdueLoansFeeJob>(jobBuilder => jobBuilder.WithIdentity(JobKey))
                .AddTrigger(q =>
                {
                    q.ForJob(JobKey);
                    q.WithCronSchedule("0 0 * * * ?");
                });
        }
    }
}
