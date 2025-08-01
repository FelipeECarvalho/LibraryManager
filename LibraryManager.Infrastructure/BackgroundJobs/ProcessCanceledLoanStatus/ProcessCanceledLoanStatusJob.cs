﻿namespace LibraryManager.Infrastructure.BackgroundJobs.ProcessCanceledLoanStatus
{
    using LibraryManager.Core.Abstractions.Repositories;
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Enums;
    using LibraryManager.Infrastructure.Email;
    using LibraryManager.Infrastructure.Email.Emails;
    using Microsoft.Extensions.Logging;
    using Quartz;
    using System.Threading.Tasks;

    internal sealed class ProcessCanceledLoanStatusJob : IJob
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly ILogger<ProcessCanceledLoanStatusJob> _logger;

        public ProcessCanceledLoanStatusJob(
            ILoanRepository loanRepository,
            IUnitOfWork unitOfWork,
            IEmailService emailService,
            ILogger<ProcessCanceledLoanStatusJob> logger)
        {
            _loanRepository = loanRepository;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Processing BackgroundJob: ProcessCanceledLoanStatusJob. {DateTimeUtc}", DateTimeOffset.UtcNow);

            try
            {
                var loans = await _loanRepository
                    .GetByStatusAsync(LoanStatus.Approved);

                _logger.LogInformation("Total loans to process: {Count}", loans.Count);

                await ProcessLoans(loans);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Completed BackgroundJob: ProcessCanceledLoanStatusJob. {DateTimeUtc}", DateTimeOffset.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Completed BackgroundJob: ProcessCanceledLoanStatusJob with error. {DateTimeUtc}", DateTimeOffset.UtcNow);
            }
        }

        private async Task ProcessLoans(IList<Loan> loans)
        {
            foreach (var loan in loans)
            {
                if (loan.CanBeCanceled())
                {
                    loan.UpdateStatus(LoanStatus.Cancelled);

                    var email = new LoanCanceledEmail(loan);
                    await _emailService.SendAsync(email);
                }
            }
        }
    }
}
