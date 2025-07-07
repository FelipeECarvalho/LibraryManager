namespace LibraryManager.Infrastructure.BackgroundJobs.ProcessCanceledLoanStatus
{
    using LibraryManager.Core.Abstractions.Repositories;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;
    using Quartz;

    internal sealed class ProcessCanceledLoanStatusJob : IJob
    {
        private readonly ILoanRepository _loanRepository;
        private readonly ILogger<ProcessCanceledLoanStatusJob> _logger;

        public ProcessCanceledLoanStatusJob(
            ILoanRepository loanRepository, 
            ILogger<ProcessCanceledLoanStatusJob> logger)
        {
            _loanRepository = loanRepository;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Processing BackgroundJob: ProcessCanceledLoanStatusJob. {@DateTimeUtc}", DateTime.UtcNow);

            try
            {
                await _loanRepository.ProcessCanceledAsync();

                _logger.LogInformation("Completed BackgroundJob: ProcessCanceledLoanStatusJob. {@DateTimeUtc}", DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                 _logger.LogError(ex, "Completed BackgroundJob: ProcessCanceledLoanStatusJob with error. {@DateTimeUtc}", DateTime.UtcNow);
            }

            return;
        }
    }
}
