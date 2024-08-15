using System.ComponentModel.DataAnnotations;

namespace TechWave.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter a username.")]
        [StringLength(255)]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a email.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a Phone number.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "The phone number must be exactly 10 digits.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a password.")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
