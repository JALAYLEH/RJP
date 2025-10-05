using System.ComponentModel.DataAnnotations;

namespace SubscriptionManagement.Models.DTO.User
{
    public class UserInputDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "Name can't exceed 100 characters")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [MaxLength(150)]
        public string Email { get; set; }
    }
}
