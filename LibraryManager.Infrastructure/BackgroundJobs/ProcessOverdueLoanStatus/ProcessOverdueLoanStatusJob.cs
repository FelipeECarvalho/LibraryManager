namespace LibraryManager.Infrastructure.BackgroundJobs.ProcessOverdueLoanStatus
{
    using LibraryManager.Application.UseCases;
    using Microsoft.Extensions.Logging;
    using Quartz;
    using System.Threading.Tasks;

    internal sealed class ProcessOverdueLoanStatusJob : IJob
    {
        private readonly OverdueLoansUseCase _overdueLoansUseCase;
        private readonly ILogger<ProcessOverdueLoanStatusJob> _logger;

        public ProcessOverdueLoanStatusJob(
            OverdueLoansUseCase overdueLoansUseCase,
            ILogger<ProcessOverdueLoanStatusJob> logger)
        {
            _overdueLoansUseCase = overdueLoansUseCase;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Starting BackgroundJob: ProcessOverdueLoanStatusJob...");

            try
            {
                await _overdueLoansUseCase.ExecuteAsync();

                _logger.LogInformation("Completed BackgroundJob: ProcessOverdueLoanStatusJob.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BackgroundJob ProcessOverdueLoanStatusJob failed.");
            }
        }
    }
}
