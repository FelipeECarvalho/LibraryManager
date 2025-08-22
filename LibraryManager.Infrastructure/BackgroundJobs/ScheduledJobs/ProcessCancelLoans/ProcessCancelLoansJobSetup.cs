namespace LibraryManager.Infrastructure.BackgroundJobs.ScheduledJobs.ProcessCancelLoans
{
    using Microsoft.Extensions.Options;
    using Quartz;

    internal sealed class ProcessCancelLoansJobSetup : IConfigureOptions<QuartzOptions>
    {
        private static readonly JobKey JobKey = JobKey.Create(nameof(ProcessCancelLoansJob));
        private readonly JobSchedulesOptions _schedules;

        public ProcessCancelLoansJobSetup(
            IOptions<JobSchedulesOptions> options)
        {
            _schedules = options.Value;
        }

        public void Configure(QuartzOptions options)
        {
            options
                .AddJob<ProcessCancelLoansJob>(jobBuilder => jobBuilder.WithIdentity(JobKey))
                .AddTrigger(q =>
                {
                    q.ForJob(JobKey);
                    q.WithCronSchedule(_schedules.ProcessCancelLoansJob);
                });
        }
    }
}
