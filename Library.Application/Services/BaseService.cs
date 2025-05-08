namespace Library.Application.Services
{
    public abstract class BaseService<T>
    {
        public abstract Task<IList<T>> GetAllAsync();

        public abstract Task<T> GetByIdAsync(int id);

        public abstract Task<T> CreateAsync(T book);

        public abstract Task UpdateAsync(T book);

        public abstract Task DeleteAsync(T book);
    }
}
