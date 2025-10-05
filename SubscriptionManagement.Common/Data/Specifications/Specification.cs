using SubscriptionManagement.Common.Data.Interfaces;
using System.Linq.Expressions;

namespace SubscriptionManagement.Common.Data.Specifications
{

    public class Specification<T> : ISpecification<T>
    {
        public Specification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; }
    }

}
