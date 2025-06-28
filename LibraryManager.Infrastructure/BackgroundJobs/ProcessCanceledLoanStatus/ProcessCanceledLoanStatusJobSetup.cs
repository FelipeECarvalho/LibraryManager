namespace LibraryManager.Infrastructure.BackgroundJobs.ProcessCanceledLoanStatus
{
    using Microsoft.Extensions.Options;
    using Quartz;

    internal sealed class ProcessCanceledLoanStatusJobSetup : IConfigureOptions<QuartzOptions>
    {
        private static readonly JobKey JobKey = JobKey.Create(nameof(ProcessCanceledLoanStatusJob));

        public void Configure(QuartzOptions options)
        {
            options
                .AddJob<ProcessCanceledLoanStatusJob>(jobBuilder => jobBuilder.WithIdentity(JobKey))
                .AddTrigger(q =>
                {
                    q.ForJob(JobKey);
                    q.WithCronSchedule("0 0 * * * ?");
                });
        }
    }
}
