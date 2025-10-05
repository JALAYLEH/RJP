using System.ComponentModel.DataAnnotations;

namespace SubscriptionManagement.Models.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        // Navigation property
        public ICollection<Subscription> Subscriptions { get; set; }
    }
}
