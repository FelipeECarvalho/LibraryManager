namespace LibraryManager.Infrastructure.BackgroundJobs.ProcessOverdueLoans
{
    using Microsoft.Extensions.Options;
    using Quartz;

    internal sealed class ProcessOverdueLoansJobSetup : IConfigureOptions<QuartzOptions>
    {
        private static readonly JobKey JobKey = JobKey.Create(nameof(ProcessOverdueLoansJob));

        public void Configure(QuartzOptions options)
        {
            options
                .AddJob<ProcessOverdueLoansJob>(jobBuilder => jobBuilder.WithIdentity(JobKey))
                .AddTrigger(q =>
                {
                    q.ForJob(JobKey);
                    q.WithCronSchedule("0 0 * * * ?");
                });
        }
    }
}
