namespace LibraryManager.Application.Abstractions
{
    public interface ITransaction
    {
        Task ExecuteWithRetryAsync(Func<CancellationToken, Task> operation, CancellationToken cancellationToken = default);
    }
}
