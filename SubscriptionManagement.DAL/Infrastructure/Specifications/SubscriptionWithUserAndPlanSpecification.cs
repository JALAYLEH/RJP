using SubscriptionManagement.Models.Entities;

namespace SubscriptionManagement.DAL.Infrastructure.Specifications
{
    public class SubscriptionWithUserAndPlanSpecification : BaseSpecification<Subscription>
    {
        public SubscriptionWithUserAndPlanSpecification()
        {
            AddInclude(s => s.User);
            AddInclude(s => s.Plan);
        }

        public SubscriptionWithUserAndPlanSpecification(Guid id)
            : base(s => s.Id == id)
        {
            AddInclude(s => s.User);
            AddInclude(s => s.Plan);
        }
    }
}
