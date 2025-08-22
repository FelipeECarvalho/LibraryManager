namespace LibraryManager.Infrastructure.BackgroundJobs.ScheduledJobs.ProcessOverdueLoans
{
    using LibraryManager.Application.Abstractions;
    using LibraryManager.Application.UseCases;
    using Microsoft.Extensions.Options;
    using Quartz;
    using System.Threading.Tasks;

    internal sealed class ProcessOverdueLoansJob(
        OverdueLoansUseCase overdueLoansUseCase,
        IAppLogger<ProcessOverdueLoansJob> logger,
        IOptions<BackgroundJobOptions> options) : ResilientJob<ProcessOverdueLoansJob>(logger, options)
    {
        public override async Task ExecuteCore(IJobExecutionContext context) => await overdueLoansUseCase.ExecuteAsync();
    }
}
