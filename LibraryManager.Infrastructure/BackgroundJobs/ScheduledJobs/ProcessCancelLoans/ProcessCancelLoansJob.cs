namespace LibraryManager.Infrastructure.BackgroundJobs.ScheduledJobs.ProcessCancelLoans
{
    using LibraryManager.Application.Abstractions;
    using LibraryManager.Application.UseCases;
    using Microsoft.Extensions.Options;
    using Quartz;
    using System.Threading.Tasks;

    internal sealed class ProcessCancelLoansJob(
        CancelLoansUseCase cancelLoansUseCase,
        IAppLogger<ProcessCancelLoansJob> logger,
        IOptions<BackgroundJobOptions> options) : ResilientJob<ProcessCancelLoansJob>(logger, options)
    {
        public override async Task ExecuteCore(IJobExecutionContext context) => await cancelLoansUseCase.ExecuteAsync();
    }
}
