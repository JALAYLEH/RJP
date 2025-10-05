using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace SubscriptionManagement.Models.ViewModels.Subscription
{
    public class SubscriptionViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "User")]
        public Guid UserId { get; set; }

        [Required]
        [Display(Name = "Plan")]
        public Guid PlanId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Required]
        [Display(Name = "Status")]
        public string Status { get; set; }
        public List<SelectListItem>? Users { get; set; }
        public List<SelectListItem>? Plans { get; set; }
        public List<SelectListItem>? StatusList { get; set; }
    }
}
