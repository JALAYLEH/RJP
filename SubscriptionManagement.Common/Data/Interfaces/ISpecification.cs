using System.Linq.Expressions;

namespace SubscriptionManagement.Common.Data.Interfaces
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
    }
}
