using SubscriptionManagement.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SubscriptionManagement.Models.Entities
{
    public class Subscription
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid PlanId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public SubscriptionStatus Status { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("PlanId")]
        public Plan Plan { get; set; }

    }
}
