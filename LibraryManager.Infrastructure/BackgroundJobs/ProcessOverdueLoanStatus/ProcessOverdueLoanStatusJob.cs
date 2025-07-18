namespace LibraryManager.Infrastructure.BackgroundJobs.ProcessOverdueLoanStatus
{
    using LibraryManager.Core.Abstractions.Repositories;
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Enums;
    using LibraryManager.Infrastructure.Email;
    using Microsoft.Extensions.Logging;
    using Quartz;
    using System.Threading.Tasks;

    internal sealed class ProcessOverdueLoanStatusJob : IJob
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProcessOverdueLoanStatusJob> _logger;
        private readonly IEmailService _emailService;

        public ProcessOverdueLoanStatusJob(
            ILoanRepository loanRepository,
            IUnitOfWork unitOfWork,
            IEmailService emailService,
            ILogger<ProcessOverdueLoanStatusJob> logger)
        {
            _loanRepository = loanRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _emailService = emailService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Processing BackgroundJob: ProcessOverdueLoanStatusJob. {DateTimeUtc}", DateTime.UtcNow);

            try
            {
                var loans = await _loanRepository.GetByStatusAsync(LoanStatus.Borrowed);

                _logger.LogInformation("Total loans to process: {Count}", loans.Count);

                await ProcessLoans(loans);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Completed BackgroundJob: ProcessOverdueLoanStatusJob. {DateTimeUtc}", DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Completed BackgroundJob: ProcessOverdueLoanStatusJob with error. {DateTimeUtc}", DateTime.UtcNow);
            }
        }

        private async Task ProcessLoans(IList<Loan> loans)
        {
            foreach (var loan in loans)
            {
                if (loan.IsNearOverdue())
                {
                    await _emailService.SendAsync(null);
                }

                if (loan.CanBeOverdue())
                {
                    loan.UpdateStatus(LoanStatus.Overdue);
                    loan.UpdateOverdueFee();

                    await _emailService.SendAsync(null);
                }
            }
        }
    }
}
