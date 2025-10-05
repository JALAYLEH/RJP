using Microsoft.EntityFrameworkCore;
using SubscriptionManagement.Models.Entities;

namespace SubscriptionManagement.DAL.AppDBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
    }
}
