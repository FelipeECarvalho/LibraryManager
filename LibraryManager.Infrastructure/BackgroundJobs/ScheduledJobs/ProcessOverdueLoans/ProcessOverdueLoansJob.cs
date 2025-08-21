namespace LibraryManager.Infrastructure.BackgroundJobs.ScheduledJobs.ProcessOverdueLoans
{
    using LibraryManager.Application.Abstractions;
    using LibraryManager.Application.UseCases;
    using Quartz;
    using System.Threading.Tasks;

    internal sealed class ProcessOverdueLoansJob(
        OverdueLoansUseCase overdueLoansUseCase,
        IAppLogger<ProcessOverdueLoansJob> logger) : ResilientJob<ProcessOverdueLoansJob>(logger)
    {
        public override async Task ExecuteCore(IJobExecutionContext context) => await overdueLoansUseCase.ExecuteAsync();
    }
}
