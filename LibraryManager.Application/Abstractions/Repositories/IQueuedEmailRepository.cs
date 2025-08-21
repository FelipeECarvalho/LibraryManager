namespace LibraryManager.Application.Abstractions.Repositories
{
    using LibraryManager.Application.Models;

    public interface IQueuedEmailRepository
    {
        void Add(QueuedEmail queuedEmail);
    }
}