using System.ComponentModel.DataAnnotations;

namespace SGGWSupportWeb.Models
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [StringLength(32, ErrorMessage = "{0} musi mieć minimalnie {2} znaki, a maksymalnie {1}.", MinimumLength = 8)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Hasło i jego potwierdzenie się nie zgadzają.")]
        public string ConfirmPassword { get; set; }
    }
}