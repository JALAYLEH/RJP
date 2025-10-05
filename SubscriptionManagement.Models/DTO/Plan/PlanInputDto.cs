using System.ComponentModel.DataAnnotations;

namespace SubscriptionManagement.Models.DTO.Plan
{
    public class PlanInputDto
    {
        [Required(ErrorMessage = "Plan name is required")]
        [MaxLength(100, ErrorMessage = "Plan name cannot exceed 100 characters")]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price per month is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number")]
        public decimal PricePerMonth { get; set; }
    }
}
