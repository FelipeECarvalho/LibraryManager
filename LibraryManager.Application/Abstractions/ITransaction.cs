namespace LibraryManager.Application.Abstractions
{
    public interface ITransaction
    {
        Task ExecuteWithRetryAsync(Func<CancellationToken, Task> operation, CancellationToken cancellationToken = default);

        Task<T> ExecuteWithRetryAsync<T>(Func<CancellationToken, Task<T>> operation, CancellationToken cancellationToken);
    }
}
