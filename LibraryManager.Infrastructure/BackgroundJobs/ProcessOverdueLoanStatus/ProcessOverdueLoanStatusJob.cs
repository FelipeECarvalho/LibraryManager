namespace LibraryManager.Infrastructure.BackgroundJobs.ProcessOverdueLoanStatus
{
    using LibraryManager.Core.Abstractions.Repositories;
    using Microsoft.Extensions.Logging;
    using Quartz;
    using System.Threading.Tasks;

    internal sealed class ProcessOverdueLoanStatusJob : IJob
    {
        private readonly ILoanRepository _loanRepository;
        private readonly ILogger<ProcessOverdueLoanStatusJob> _logger;

        public ProcessOverdueLoanStatusJob(
            ILoanRepository loanRepository,
            ILogger<ProcessOverdueLoanStatusJob> logger)
        {
            _loanRepository = loanRepository;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {

            _logger.LogInformation("Processing BackgroundJob: ProcessOverdueLoanStatusJob. {@DateTimeUtc}", DateTime.UtcNow);

            try
            {
                await _loanRepository.ProcessOverdueAsync();

                _logger.LogInformation("Completed BackgroundJob: ProcessOverdueLoanStatusJob. {@DateTimeUtc}", DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Completed BackgroundJob: ProcessOverdueLoanStatusJob with error. {@DateTimeUtc}", DateTime.UtcNow);
            }

            return;
        }
    }
}
