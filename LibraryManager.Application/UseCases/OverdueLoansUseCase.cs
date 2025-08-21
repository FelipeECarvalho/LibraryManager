namespace LibraryManager.Application.UseCases
{
    using LibraryManager.Application.Abstractions;
    using LibraryManager.Application.Abstractions.Email;
    using LibraryManager.Application.Abstractions.Repositories;
    using LibraryManager.Application.Notifications;
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Enums;

    public sealed class OverdueLoansUseCase
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppLogger<OverdueLoansUseCase> _logger;
        private readonly IEmailService _emailService;

        public OverdueLoansUseCase(
            ILoanRepository loanRepository,
            IUnitOfWork unitOfWork,
            IEmailService emailService,
            IAppLogger<OverdueLoansUseCase> logger)
        {
            _loanRepository = loanRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _emailService = emailService;
        }

        public async Task ExecuteAsync()
        {
            var loans = await _loanRepository
                .GetByStatusAsync(LoanStatus.Borrowed);

            _logger.LogInformation("Total loans to process: {Count}", loans.Count);

            await ProcessLoans(loans);

            await _unitOfWork.SaveChangesAsync();
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
            await _emailService.SendAsync(email);
        }
    }
}
