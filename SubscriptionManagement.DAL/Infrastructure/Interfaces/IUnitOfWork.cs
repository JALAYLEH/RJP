using SubscriptionManagement.DAL.Repositories.Interfaces;

namespace SubscriptionManagement.DAL.Infrasructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IPlanRepository Plans { get; }
        public IUserRepository Users { get; }
        public ISubscriptionRepository Subscriptions { get; }
        IRepository<T> Repository<T>() where T : class;
        Task<int> CompleteAsync();

    }
}
