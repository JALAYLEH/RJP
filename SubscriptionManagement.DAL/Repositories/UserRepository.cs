using Microsoft.EntityFrameworkCore;
using SubscriptionManagement.DAL.AppDBContext;
using SubscriptionManagement.DAL.Infrastructure;
using SubscriptionManagement.DAL.Repositories.Interfaces;
using SubscriptionManagement.Models.Entities;

namespace SubscriptionManagement.DAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            var user = await _context.Set<User>()
                        .FirstOrDefaultAsync(u => u.Email == email);


            return user;
        }
    }
}
