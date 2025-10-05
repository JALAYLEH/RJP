using SubscriptionManagement.DAL.AppDBContext;
using SubscriptionManagement.DAL.Infrasructure.Interfaces;
using SubscriptionManagement.DAL.Infrastructure;
using SubscriptionManagement.DAL.Repositories;
using SubscriptionManagement.DAL.Repositories.Interfaces;

namespace SubscriptionManagement.DAL.Infrasructure.UnitOfWork
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IPlanRepository Plans { get; }
        public IUserRepository Users { get; }
        public ISubscriptionRepository Subscriptions { get; }
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Plans = new PlanRepository(_context);
            Users = new UserRepository(_context);
            Subscriptions = new SubscriptionRepository(_context);
        }

        public IRepository<T> Repository<T>() where T : class
        {
            return new Repository<T>(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
