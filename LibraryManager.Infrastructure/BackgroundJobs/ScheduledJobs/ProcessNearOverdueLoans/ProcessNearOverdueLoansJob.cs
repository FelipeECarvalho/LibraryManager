namespace LibraryManager.Infrastructure.BackgroundJobs.ScheduledJobs.ProcessNearOverdueLoans
{
    using LibraryManager.Application.Abstractions;
    using LibraryManager.Application.UseCases;
    using Quartz;
    using System.Threading.Tasks;

    internal sealed class ProcessNearOverdueLoansJob(
        NotifyNearOverdueLoansUseCase notifyNearOverdueLoansUseCase,
        IAppLogger<ProcessNearOverdueLoansJob> logger) : ResilientJob<ProcessNearOverdueLoansJob>(logger)
    {
        public override async Task ExecuteCore(IJobExecutionContext context) => await notifyNearOverdueLoansUseCase.ExecuteAsync();
    }
}
