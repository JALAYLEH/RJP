namespace SubscriptionManagement.Common.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> ListAllAsync();
        Task<IEnumerable<T>> ListAsync(ISpecification<T> spec);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
