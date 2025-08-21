namespace LibraryManager.Infrastructure.BackgroundJobs.ScheduledJobs.ProcessOverdueLoansFee
{
    using LibraryManager.Application.Abstractions;
    using LibraryManager.Application.UseCases;
    using Quartz;
    using System.Threading.Tasks;

    internal sealed class ProcessOverdueLoansFeeJob : IJob
    {
        private readonly OverdueLoansFeeUseCase _overdueLoansFeeUseCase;
        private readonly IAppLogger<ProcessOverdueLoansFeeJob> _logger;

        public ProcessOverdueLoansFeeJob(
            OverdueLoansFeeUseCase overdueLoansFeeUseCase,
            IAppLogger<ProcessOverdueLoansFeeJob> logger)
        {
            _overdueLoansFeeUseCase = overdueLoansFeeUseCase;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Starting BackgroundJob: ProcessOverdueLoansFeeJob...");

            try
            {
                await _overdueLoansFeeUseCase.ExecuteAsync();

                _logger.LogInformation("Completed BackgroundJob: ProcessOverdueLoansFeeJob.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BackgroundJob ProcessOverdueLoansFeeJob failed.");
            }
        }
    }
}
