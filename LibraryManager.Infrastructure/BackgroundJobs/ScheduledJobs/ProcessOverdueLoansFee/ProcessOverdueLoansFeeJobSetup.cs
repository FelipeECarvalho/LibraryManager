namespace LibraryManager.Infrastructure.BackgroundJobs.ScheduledJobs.ProcessOverdueLoansFee
{
    using Microsoft.Extensions.Options;
    using Quartz;

    internal sealed class ProcessOverdueLoansFeeJobSetup : IConfigureOptions<QuartzOptions>
    {
        private static readonly JobKey JobKey = JobKey.Create(nameof(ProcessOverdueLoansFeeJob));
        private readonly JobSchedulesOptions _schedules;

        public ProcessOverdueLoansFeeJobSetup(
            IOptions<JobSchedulesOptions> options)
        {
            _schedules = options.Value;
        }

        public void Configure(QuartzOptions options)
        {
            options
                .AddJob<ProcessOverdueLoansFeeJob>(jobBuilder => jobBuilder.WithIdentity(JobKey))
                .AddTrigger(q =>
                {
                    q.ForJob(JobKey);
                    q.WithCronSchedule(_schedules.ProcessOverdueLoansFeeJob);
                });
        }
    }
}
