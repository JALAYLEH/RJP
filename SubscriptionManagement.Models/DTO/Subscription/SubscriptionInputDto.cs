using SubscriptionManagement.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SubscriptionManagement.Models.DTO.Subscription
{
    public class SubscriptionInputDto
    {
        [Required(ErrorMessage = "Plan Id is required")]
        public Guid PlanId { get; set; }
        [Required(ErrorMessage = "User Id is required")]
        public Guid UserId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public SubscriptionStatus Status { get; set; }
    }
}
