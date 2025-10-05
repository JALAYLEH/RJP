using SubscriptionManagement.DAL.Infrasructure.Interfaces;
using SubscriptionManagement.Models.Entities;

namespace SubscriptionManagement.DAL.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUserByEmailAsync(string email);
    }
}
