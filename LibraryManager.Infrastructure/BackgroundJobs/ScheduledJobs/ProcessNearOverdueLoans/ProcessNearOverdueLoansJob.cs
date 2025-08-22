namespace LibraryManager.Infrastructure.BackgroundJobs.ScheduledJobs.ProcessNearOverdueLoans
{
    using LibraryManager.Application.Abstractions;
    using LibraryManager.Application.UseCases;
    using Microsoft.Extensions.Options;
    using Quartz;
    using System.Threading.Tasks;

    internal sealed class ProcessNearOverdueLoansJob(
        NotifyNearOverdueLoansUseCase notifyNearOverdueLoansUseCase,
        IAppLogger<ProcessNearOverdueLoansJob> logger,
        IOptions<BackgroundJobOptions> options) : ResilientJob<ProcessNearOverdueLoansJob>(logger, options)
    {
        public override async Task ExecuteCore(IJobExecutionContext context) => await notifyNearOverdueLoansUseCase.ExecuteAsync();
    }
}
