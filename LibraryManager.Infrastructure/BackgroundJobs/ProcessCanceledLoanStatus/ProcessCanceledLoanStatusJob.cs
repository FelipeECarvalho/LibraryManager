namespace LibraryManager.Infrastructure.BackgroundJobs.ProcessCanceledLoanStatus
{
    using LibraryManager.Core.Abstractions.Repositories;
    using LibraryManager.Core.Enums;
    using Microsoft.Extensions.Logging;
    using Quartz;
    using System.Threading.Tasks;

    internal sealed class ProcessCanceledLoanStatusJob : IJob
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProcessCanceledLoanStatusJob> _logger;

        public ProcessCanceledLoanStatusJob(
            ILoanRepository loanRepository,
            IUnitOfWork unitOfWork,
            ILogger<ProcessCanceledLoanStatusJob> logger)
        {
            _loanRepository = loanRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Processing BackgroundJob: ProcessCanceledLoanStatusJob. {@DateTimeUtc}", DateTime.UtcNow);

            try
            {
                var loans = await _loanRepository
                    .GetByStatusAsync(LoanStatus.Approved);

                _logger.LogInformation("Total loans to process: {Count}", loans.Count);

                foreach (var loan in loans) 
                {
                    if (!loan.CanBeCanceled())
                    {
                        continue;
                    }

                    loan.UpdateStatus(LoanStatus.Cancelled);

                    // TODO: Send an email to the borrower and to the library
                }

                await _unitOfWork.SaveChangesAsync();

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
