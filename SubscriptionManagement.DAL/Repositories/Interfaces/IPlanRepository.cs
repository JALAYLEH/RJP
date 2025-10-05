using SubscriptionManagement.DAL.Infrasructure.Interfaces;
using SubscriptionManagement.Models.Entities;

namespace SubscriptionManagement.DAL.Repositories.Interfaces
{
    public interface IPlanRepository : IRepository<Plan>
    {
        // Plan specific methods
        Task<Plan?> GetPlanWithSubscriptionsAsync(Guid id);
    }
}
