namespace LibraryManager.Infrastructure.Resilience
{
    public interface ITransaction
    {
        Task ExecuteWithRetryAsync(Func<CancellationToken?, Task> operation, CancellationToken cancellationToken = default);
    }
}
