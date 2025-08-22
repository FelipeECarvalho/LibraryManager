namespace LibraryManager.Infrastructure.BackgroundJobs.ScheduledJobs.ProcessOverdueLoans
{
    using Microsoft.Extensions.Options;
    using Quartz;

    internal sealed class ProcessOverdueLoansJobSetup : IConfigureOptions<QuartzOptions>
    {
        private static readonly JobKey JobKey = JobKey.Create(nameof(ProcessOverdueLoansJob));
        private readonly JobSchedulesOptions _schedules;

        public ProcessOverdueLoansJobSetup(
            IOptions<JobSchedulesOptions> options)
        {
            _schedules = options.Value;
        }

        public void Configure(QuartzOptions options)
        {
            options
                .AddJob<ProcessOverdueLoansJob>(jobBuilder => jobBuilder.WithIdentity(JobKey))
                .AddTrigger(q =>
                {
                    q.ForJob(JobKey);
                    q.WithCronSchedule(_schedules.ProcessOverdueLoansJob);
                });
        }
    }
}
