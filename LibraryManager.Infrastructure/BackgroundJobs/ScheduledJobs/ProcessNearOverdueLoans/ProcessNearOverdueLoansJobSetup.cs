namespace LibraryManager.Infrastructure.BackgroundJobs.ScheduledJobs.ProcessNearOverdueLoans
{
    using Microsoft.Extensions.Options;
    using Quartz;

    internal sealed class ProcessNearOverdueLoansJobSetup : IConfigureOptions<QuartzOptions>
    {
        private static readonly JobKey JobKey = JobKey.Create(nameof(ProcessNearOverdueLoansJob));
        private readonly JobSchedulesOptions _schedules;

        public ProcessNearOverdueLoansJobSetup(
            IOptions<JobSchedulesOptions> options)
        {
            _schedules = options.Value;
        }

        public void Configure(QuartzOptions options)
        {
            options
                .AddJob<ProcessNearOverdueLoansJob>(jobBuilder => jobBuilder.WithIdentity(JobKey))
                .AddTrigger(q =>
                {
                    q.ForJob(JobKey);
                    q.WithCronSchedule(_schedules.ProcessNearOverdueLoansJob);
                });
        }
    }
}
