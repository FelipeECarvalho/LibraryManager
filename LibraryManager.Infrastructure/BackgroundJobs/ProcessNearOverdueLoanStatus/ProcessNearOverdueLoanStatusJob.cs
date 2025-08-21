namespace LibraryManager.Infrastructure.BackgroundJobs.ProcessNearOverdueLoanStatus
{
    using LibraryManager.Application.Interfaces;
    using LibraryManager.Application.Interfaces.Repositories;
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Enums;
    using LibraryManager.Infrastructure.Email.Emails;
    using Microsoft.Extensions.Logging;
    using Quartz;
    using System.Threading.Tasks;

    internal sealed class ProcessNearOverdueLoanStatusJob : IJob
    {
        private readonly ILoanRepository _loanRepository;
        private readonly ILogger<ProcessNearOverdueLoanStatusJob> _logger;
        private readonly IEmailService _emailService;

        public ProcessNearOverdueLoanStatusJob(
            ILoanRepository loanRepository,
            IEmailService emailService,
            ILogger<ProcessNearOverdueLoanStatusJob> logger)
        {
            _loanRepository = loanRepository;
            _logger = logger;
            _emailService = emailService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Processing BackgroundJob: ProcessNearOverdueLoanStatusJob. {DateTimeUtc}", DateTime.UtcNow);

            try
            {
                var loans = await _loanRepository
                    .GetByStatusAsync(LoanStatus.Borrowed);

                _logger.LogInformation("Total loans to process: {Count}", loans.Count);

                await ProcessLoans(loans);

                _logger.LogInformation("Completed BackgroundJob: ProcessNearOverdueLoanStatusJob. {DateTimeUtc}", DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Completed BackgroundJob: ProcessNearOverdueLoanStatusJob with error. {DateTimeUtc}", DateTime.UtcNow);
            }
        }

        private async Task ProcessLoans(IList<Loan> loans)
        {
            var nearOverdueLoans = loans
                .Where(x => x.IsNearOverdue());

            var processingTasks = nearOverdueLoans.Select(async loan =>
            {
                try
                {
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
            var email = new LoanNearOverdueEmail(loan);
            await _emailService.SendAsync(email.To, email.Subject, email.Body);
        }
    }
}
