using System.ComponentModel.DataAnnotations;

namespace SubscriptionManagement.Models.ViewModels.Plan
{
    public class PlanViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Plan name is required")]
        [MaxLength(100, ErrorMessage = "Plan name cannot exceed 100 characters")]
        [Display(Name = "Plan Name")]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        [Display(Name = "Plan Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price per month is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number")]
        [Display(Name = "Monthly Price")]
        public decimal PricePerMonth { get; set; }


        public string FormattedPrice => $"${PricePerMonth:N2}";
    }
}
