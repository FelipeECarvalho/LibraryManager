namespace LibraryManager.Application.Interfaces.Repositories
{
    using LibraryManager.Application.Models;

    public interface IQueuedEmailRepository
    {
        void Add(QueuedEmail queuedEmail);
    }
}