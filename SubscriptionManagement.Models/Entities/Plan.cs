using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SubscriptionManagement.Models.Entities
{
    public class Plan
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Plan name is required")]
        [MaxLength(100, ErrorMessage = "Plan name cannot exceed 100 characters")]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price per month is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerMonth { get; set; }
        // Navigation property
        public ICollection<Subscription> Subscriptions { get; set; }
    }
}
