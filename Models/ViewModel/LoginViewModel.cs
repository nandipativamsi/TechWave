using System.ComponentModel.DataAnnotations;

namespace TechWave.Models.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter a Email.")]
        [StringLength(255)]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter a password.")]
        [StringLength(255)]
        public string Password { get; set; } = string.Empty;
        public string ReturnUrl { get; set; } = string.Empty;
    }
}
