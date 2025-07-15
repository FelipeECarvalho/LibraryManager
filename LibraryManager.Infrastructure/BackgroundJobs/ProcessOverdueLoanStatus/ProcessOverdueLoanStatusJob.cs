namespace LibraryManager.Infrastructure.BackgroundJobs.ProcessOverdueLoanStatus
{
    using LibraryManager.Core.Abstractions.Repositories;
    using LibraryManager.Core.Enums;
    using Microsoft.Extensions.Logging;
    using Quartz;
    using System.Threading.Tasks;

    internal sealed class ProcessOverdueLoanStatusJob : IJob
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProcessOverdueLoanStatusJob> _logger;

        public ProcessOverdueLoanStatusJob(
            ILoanRepository loanRepository,
            IUnitOfWork unitOfWork,
            ILogger<ProcessOverdueLoanStatusJob> logger)
        {
            _loanRepository = loanRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Processing BackgroundJob: ProcessOverdueLoanStatusJob. {@DateTimeUtc}", DateTime.UtcNow);

            try
            {
                var loans = await _loanRepository.GetByStatusAsync(LoanStatus.Borrowed);

                _logger.LogInformation("Total loans to process: {Count}", loans.Count);

                foreach (var loan in loans) 
                {
                    if (!loan.CanBeOverdue())
                    {
                        continue;
                    }

                    loan.UpdateStatus(LoanStatus.Overdue);
                }

                await _unitOfWork.SaveChangesAsync();

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
