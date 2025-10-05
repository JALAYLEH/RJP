namespace SubscriptionManagement.DAL.Infrasructure.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id);
        Task<T> GetByIdAsync(Guid id, ISpecification<T> spec);
        Task<IEnumerable<T>> ListAllAsync();
        Task<IEnumerable<T>> ListAsync(ISpecification<T> spec);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
