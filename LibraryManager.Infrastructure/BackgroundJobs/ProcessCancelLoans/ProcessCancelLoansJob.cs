namespace LibraryManager.Infrastructure.BackgroundJobs.ProcessCancelLoans
{
    using LibraryManager.Application.UseCases;
    using Microsoft.Extensions.Logging;
    using Quartz;
    using System.Threading.Tasks;

    internal sealed class ProcessCancelLoansJob : IJob
    {
        private readonly CancelLoansUseCase _cancelLoansUseCase;
        private readonly ILogger<ProcessCancelLoansJob> _logger;

        public ProcessCancelLoansJob(
            CancelLoansUseCase cancelLoansUseCase,
            ILogger<ProcessCancelLoansJob> logger)
        {
            _logger = logger;
            _cancelLoansUseCase = cancelLoansUseCase;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Starting BackgroundJob: ProcessCancelLoansJob...");

            try
            {
                await _cancelLoansUseCase.ExecuteAsync();

                _logger.LogInformation("Completed BackgroundJob: ProcessCancelLoansJob.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BackgroundJob ProcessCancelLoansJob failed.");
            }
        }
    }
}
