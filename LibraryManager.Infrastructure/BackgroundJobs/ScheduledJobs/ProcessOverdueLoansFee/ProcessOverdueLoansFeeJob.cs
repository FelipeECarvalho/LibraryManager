namespace LibraryManager.Infrastructure.BackgroundJobs.ScheduledJobs.ProcessOverdueLoansFee
{
    using LibraryManager.Application.Abstractions;
    using LibraryManager.Application.UseCases;
    using Quartz;
    using System.Threading.Tasks;

    internal sealed class ProcessOverdueLoansFeeJob(
        OverdueLoansFeeUseCase overdueLoansFeeUseCase,
        IAppLogger<ProcessOverdueLoansFeeJob> logger) : ResilientJob<ProcessOverdueLoansFeeJob>(logger)
    {
        public override async Task ExecuteCore(IJobExecutionContext context) => await overdueLoansFeeUseCase.ExecuteAsync();
    }
}
