namespace LibraryManager.Infrastructure.BackgroundJobs.ProcessCanceledLoanStatus
{
    using LibraryManager.Application.UseCases;
    using Microsoft.Extensions.Logging;
    using Quartz;
    using System.Threading.Tasks;

    internal sealed class ProcessCanceledLoanStatusJob : IJob
    {
        private readonly CancelLoansUseCase _cancelLoansUseCase;
        private readonly ILogger<ProcessCanceledLoanStatusJob> _logger;

        public ProcessCanceledLoanStatusJob(
            CancelLoansUseCase cancelLoansUseCase,
            ILogger<ProcessCanceledLoanStatusJob> logger)
        {
            _logger = logger;
            _cancelLoansUseCase = cancelLoansUseCase;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Starting BackgroundJob: ProcessCanceledLoanStatusJob...");

            try
            {
                await _cancelLoansUseCase.ExecuteAsync();

                _logger.LogInformation("Completed BackgroundJob: ProcessCanceledLoanStatusJob.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BackgroundJob ProcessCanceledLoanStatusJob failed.");
            }
        }
    }
}
