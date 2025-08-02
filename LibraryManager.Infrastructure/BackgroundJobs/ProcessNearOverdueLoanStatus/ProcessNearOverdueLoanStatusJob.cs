namespace LibraryManager.Infrastructure.BackgroundJobs.ProcessNearOverdueLoanStatus
{
    using LibraryManager.Core.Abstractions.Repositories;
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Enums;
    using LibraryManager.Infrastructure.Email;
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
            foreach (var loan in loans)
            {
                if (loan.IsNearOverdue())
                {
                    var email = new LoanNearOverdueEmail(loan);
                    await _emailService.SendAsync(email);
                }
            }
        }
    }
}
