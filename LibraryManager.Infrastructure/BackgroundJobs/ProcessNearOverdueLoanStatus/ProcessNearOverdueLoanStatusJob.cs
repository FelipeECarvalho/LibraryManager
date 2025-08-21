namespace LibraryManager.Infrastructure.BackgroundJobs.ProcessNearOverdueLoanStatus
{
    using LibraryManager.Application.UseCases;
    using Microsoft.Extensions.Logging;
    using Quartz;
    using System.Threading.Tasks;

    internal sealed class ProcessNearOverdueLoanStatusJob : IJob
    {
        private readonly NotifyNearOverdueLoansUseCase _notifyNearOverdueLoansUseCase;
        private readonly ILogger<ProcessNearOverdueLoanStatusJob> _logger;

        public ProcessNearOverdueLoanStatusJob(
            NotifyNearOverdueLoansUseCase notifyNearOverdueLoansUseCase,
            ILogger<ProcessNearOverdueLoanStatusJob> logger)
        {
            _notifyNearOverdueLoansUseCase = notifyNearOverdueLoansUseCase;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Starting BackgroundJob: ProcessNearOverdueLoanStatusJob...");

            try
            {
                await _notifyNearOverdueLoansUseCase.ExecuteAsync();

                _logger.LogInformation("Completed BackgroundJob: ProcessNearOverdueLoanStatusJob.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BackgroundJob ProcessNearOverdueLoanStatusJob failed.");
            }
        }
    }
}
