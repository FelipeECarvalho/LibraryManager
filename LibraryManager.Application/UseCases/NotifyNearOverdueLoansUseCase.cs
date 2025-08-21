namespace LibraryManager.Application.UseCases
{
    using LibraryManager.Application.Abstractions;
    using LibraryManager.Application.Abstractions.Email;
    using LibraryManager.Application.Abstractions.Repositories;
    using LibraryManager.Application.Notifications;
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public sealed class NotifyNearOverdueLoansUseCase
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IAppLogger<NotifyNearOverdueLoansUseCase> _logger;
        private readonly IEmailService _emailService;

        public NotifyNearOverdueLoansUseCase(
            ILoanRepository loanRepository,
            IEmailService emailService,
            IAppLogger<NotifyNearOverdueLoansUseCase> logger)
        {
            _loanRepository = loanRepository;
            _logger = logger;
            _emailService = emailService;
        }

        public async Task ExecuteAsync()
        {
            var loans = await _loanRepository
                .GetByStatusAsync(LoanStatus.Borrowed);

            _logger.LogInformation("Total loans to process: {Count}", loans.Count);

            await ProcessLoans(loans);
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

                    _logger.LogInformation("The loan {LoanId} was notified", loan.Id);
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
            await _emailService.SendAsync(email);
        }
    }
}
