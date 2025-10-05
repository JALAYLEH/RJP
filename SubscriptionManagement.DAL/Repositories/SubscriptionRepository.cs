using Microsoft.EntityFrameworkCore;
using SubscriptionManagement.DAL.AppDBContext;
using SubscriptionManagement.DAL.Infrastructure;
using SubscriptionManagement.DAL.Repositories.Interfaces;
using SubscriptionManagement.Models.Entities;

namespace SubscriptionManagement.DAL.Repositories
{
    public class SubscriptionRepository : Repository<Subscription>, ISubscriptionRepository
    {
        private readonly AppDbContext _context;

        public SubscriptionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Subscription>> GetActiveSubscriptionsByUserAsync(Guid userId)
        {
            return await _context.Set<Subscription>()
                 .Include(s => s.Plan)
                 .Where(s => s.UserId == userId && s.Status == Models.Enums.SubscriptionStatus.Active)
                 .ToListAsync();
        }

    }
}
