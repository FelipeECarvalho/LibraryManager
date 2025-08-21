namespace LibraryManager.Infrastructure.BackgroundJobs.ScheduledJobs.ProcessNearOverdueLoans
{
    using LibraryManager.Application.Abstractions;
    using LibraryManager.Application.UseCases;
    using Quartz;
    using System.Threading.Tasks;

    internal sealed class ProcessNearOverdueLoansJob : IJob
    {
        private readonly NotifyNearOverdueLoansUseCase _notifyNearOverdueLoansUseCase;
        private readonly IAppLogger<ProcessNearOverdueLoansJob> _logger;

        public ProcessNearOverdueLoansJob(
            NotifyNearOverdueLoansUseCase notifyNearOverdueLoansUseCase,
            IAppLogger<ProcessNearOverdueLoansJob> logger)
        {
            _notifyNearOverdueLoansUseCase = notifyNearOverdueLoansUseCase;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Starting BackgroundJob: ProcessNearOverdueLoansJob...");

            try
            {
                await _notifyNearOverdueLoansUseCase.ExecuteAsync();

                _logger.LogInformation("Completed BackgroundJob: ProcessNearOverdueLoansJob.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BackgroundJob ProcessNearOverdueLoansJob failed.");
            }
        }
    }
}
