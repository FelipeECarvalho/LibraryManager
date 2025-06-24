namespace LibraryManager.Infrastructure.BackgroundJobs.ProcessCanceledLoanStatus
{
    using LibraryManager.Core.Abstractions.Repositories;
    using Quartz;
    using System.Threading.Tasks;

    internal sealed class ProcessCanceledLoanStatusJob : IJob
    {
        private readonly ILoanRepository _loanRepository;

        public ProcessCanceledLoanStatusJob(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        public async Task Execute(IJobExecutionContext context)
            => await _loanRepository.ProcessCanceledAsync();
    }
}
