namespace LibraryManager.Infrastructure.BackgroundJobs.ProcessOverdueLoanStatus
{
    using LibraryManager.Application.Interfaces;
    using LibraryManager.Application.Interfaces.Repositories;
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Enums;
    using LibraryManager.Infrastructure.Email.Emails;
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
            var overdueLoans = loans
                .Where(loan => loan.CanBeOverdue());

            var processingTasks = overdueLoans.Select(async loan =>
            {
                try
                {
                    loan.UpdateStatus(LoanStatus.Overdue);
                    loan.UpdateOverdueFee();

                    await SendNotificationAsync(loan);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error when processing the loan {LoanId}", loan.Id);
                }
            });

            await Task.WhenAll(processingTasks);
        }

        private async Task SendNotificationAsync(Loan loan)
        {
            var email = new LoanOverdueEmail(loan);
            await _emailService.SendAsync(email.To, email.Subject, email.Body);
        }
    }
}
