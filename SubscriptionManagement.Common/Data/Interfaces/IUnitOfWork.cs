namespace SubscriptionManagement.Common.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : class;
        Task<int> CompleteAsync();

    }
}
