using System.ComponentModel.DataAnnotations;

namespace SubscriptionManagement.Models.ViewModels.User
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        [MaxLength(100, ErrorMessage = "Full name cannot exceed 100 characters")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
    }
}
