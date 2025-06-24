namespace LibraryManager.Infrastructure.BackgroundJobs.ProcessCanceledLoanStatus
{
    using Microsoft.Extensions.Options;
    using Quartz;

    internal sealed class ProcessCanceledLoanStatusJobSetup : IConfigureOptions<QuartzOptions>
    {
        public void Configure(QuartzOptions options)
        {
            var jobKey = JobKey.Create(nameof(ProcessCanceledLoanStatusJob));

            options
                .AddJob<ProcessCanceledLoanStatusJob>(jobBuilder => jobBuilder.WithIdentity(jobKey))
                .AddTrigger(q =>
                {
                    q.ForJob(jobKey);
                    q.WithCronSchedule("0 0 * * * ?");
                });
        }
    }
}
