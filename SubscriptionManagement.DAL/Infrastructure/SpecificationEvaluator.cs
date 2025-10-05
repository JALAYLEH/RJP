using Microsoft.EntityFrameworkCore;
using SubscriptionManagement.DAL.Infrasructure.Interfaces;

namespace SubscriptionManagement.DAL.Infrastructure
{
    public static class SpecificationEvaluator<T> where T : class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            var query = inputQuery;


            if (spec?.Criteria != null)
                query = query.Where(spec.Criteria);


            if (spec?.Includes != null)
            {
                query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return query;
        }
    }
}
