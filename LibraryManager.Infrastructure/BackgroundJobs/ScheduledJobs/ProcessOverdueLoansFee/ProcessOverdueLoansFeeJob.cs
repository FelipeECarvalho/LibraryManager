namespace LibraryManager.Infrastructure.BackgroundJobs.ScheduledJobs.ProcessOverdueLoansFee
{
    using LibraryManager.Application.Abstractions;
    using LibraryManager.Application.UseCases;
    using Microsoft.Extensions.Options;
    using Quartz;
    using System.Threading.Tasks;

    internal sealed class ProcessOverdueLoansFeeJob(
        OverdueLoansFeeUseCase overdueLoansFeeUseCase,
        IAppLogger<ProcessOverdueLoansFeeJob> logger,
        IOptions<BackgroundJobOptions> options) : ResilientJob<ProcessOverdueLoansFeeJob>(logger, options)
    {
        public override async Task ExecuteCore(IJobExecutionContext context) => await overdueLoansFeeUseCase.ExecuteAsync();
    }
}
