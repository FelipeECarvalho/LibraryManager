namespace LibraryManager.Infrastructure.BackgroundJobs.ProcessOverdueLoanStatusJob
{
    using Quartz;
    using System.Threading.Tasks;

    internal sealed class ProcessOverdueLoanStatusJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.CompletedTask;
        }
    }
}
