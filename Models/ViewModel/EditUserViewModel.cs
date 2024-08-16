using System.ComponentModel.DataAnnotations;

namespace TechWave.Models.ViewModel
{
    public class EditUserViewModel
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

    }
}
