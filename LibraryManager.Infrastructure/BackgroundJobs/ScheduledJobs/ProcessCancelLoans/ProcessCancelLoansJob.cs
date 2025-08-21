namespace LibraryManager.Infrastructure.BackgroundJobs.ScheduledJobs.ProcessCancelLoans
{
    using LibraryManager.Application.Abstractions;
    using LibraryManager.Application.UseCases;
    using Quartz;
    using System.Threading.Tasks;

    internal sealed class ProcessCancelLoansJob(
        CancelLoansUseCase cancelLoansUseCase,
        IAppLogger<ProcessCancelLoansJob> logger) : ResilientJob<ProcessCancelLoansJob>(logger)
    {
        public override async Task ExecuteCore(IJobExecutionContext context) => await cancelLoansUseCase.ExecuteAsync();
    }
}
