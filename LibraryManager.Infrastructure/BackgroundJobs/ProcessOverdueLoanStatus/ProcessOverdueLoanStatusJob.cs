namespace LibraryManager.Infrastructure.BackgroundJobs.ProcessOverdueLoanStatus
{
    using LibraryManager.Core.Abstractions.Repositories;
    using Quartz;
    using System.Threading.Tasks;

    internal sealed class ProcessOverdueLoanStatusJob : IJob
    {
        private readonly ILoanRepository _loanRepository;

        public ProcessOverdueLoanStatusJob(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        public async Task Execute(IJobExecutionContext context) 
            => await _loanRepository.ProcessOverdueAsync();
    }
}
