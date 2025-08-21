namespace LibraryManager.Infrastructure.BackgroundJobs.ScheduledJobs.ProcessOverdueLoans
{
    using LibraryManager.Application.Abstractions;
    using LibraryManager.Application.UseCases;
    using Quartz;
    using System.Threading.Tasks;

    internal sealed class ProcessOverdueLoansJob : IJob
    {
        private readonly OverdueLoansUseCase _overdueLoansUseCase;
        private readonly IAppLogger<ProcessOverdueLoansJob> _logger;

        public ProcessOverdueLoansJob(
            OverdueLoansUseCase overdueLoansUseCase,
            IAppLogger<ProcessOverdueLoansJob> logger)
        {
            _overdueLoansUseCase = overdueLoansUseCase;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Starting BackgroundJob: ProcessOverdueLoansJob...");

            try
            {
                await _overdueLoansUseCase.ExecuteAsync();

                _logger.LogInformation("Completed BackgroundJob: ProcessOverdueLoansJob.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BackgroundJob ProcessOverdueLoansJob failed.");
            }
        }
    }
}
