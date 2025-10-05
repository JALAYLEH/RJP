using Microsoft.EntityFrameworkCore;
using SubscriptionManagement.DAL.AppDBContext;
using SubscriptionManagement.DAL.Infrastructure;
using SubscriptionManagement.DAL.Repositories.Interfaces;
using SubscriptionManagement.Models.Entities;

namespace SubscriptionManagement.DAL.Repositories
{
    public class PlanRepository : Repository<Plan>, IPlanRepository
    {
        private readonly AppDbContext _context;

        public PlanRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Plan?> GetPlanWithSubscriptionsAsync(Guid id)
        {
            return await _context.Set<Plan>()
                .Include(p => p.Subscriptions)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
