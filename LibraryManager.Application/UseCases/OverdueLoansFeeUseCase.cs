namespace LibraryManager.Application.UseCases
{
    using LibraryManager.Application.Abstractions;
    using LibraryManager.Application.Abstractions.Repositories;
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Enums;

    public sealed class OverdueLoansFeeUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoanRepository _loanRepository;
        private readonly IAppLogger<OverdueLoansFeeUseCase> _logger;

        public OverdueLoansFeeUseCase(
            IUnitOfWork unitOfWork,
            ILoanRepository loanRepository,
            IAppLogger<OverdueLoansFeeUseCase> logger)
        {
            _unitOfWork = unitOfWork;
            _loanRepository = loanRepository;
            _logger = logger;
        }

        public async Task ExecuteAsync()
        {
            var loans = await _loanRepository
                .GetByStatusAsync(LoanStatus.Overdue);

            _logger.LogInformation("Total loans to process: {Count}", loans.Count);

            ProcessLoans(loans);

            await _unitOfWork.SaveChangesAsync();
        }

        private void ProcessLoans(IList<Loan> loans)
        {
            foreach (var loan in loans)
            {
                loan.UpdateOverdueFee();
                _logger.LogInformation("The loan {LoanId} fee was updated to {LoanTotalOverdueFee}", loan.Id, loan.TotalOverdueFee);
            }
        }
    }
}
