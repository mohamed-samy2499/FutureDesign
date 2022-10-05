using System.ComponentModel.DataAnnotations;

namespace Project.PL.Models
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
    }
}
