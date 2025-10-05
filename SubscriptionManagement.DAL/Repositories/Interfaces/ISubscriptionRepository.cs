using SubscriptionManagement.DAL.Infrasructure.Interfaces;
using SubscriptionManagement.Models.Entities;

namespace SubscriptionManagement.DAL.Repositories.Interfaces
{
    public interface ISubscriptionRepository : IRepository<Subscription>
    {
        //specific methods for subscription repo
        //Task<Subscription> GetByIdIncludingAsync(Guid id, params Expression<Func<Subscription, object>>[] includes);

        Task<IEnumerable<Subscription>> GetActiveSubscriptionsByUserAsync(Guid userId);
    }
}
