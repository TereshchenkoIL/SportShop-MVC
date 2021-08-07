using System.ComponentModel.DataAnnotations;

namespace MySportShop.Models.ViewModel
{
    public class RegisterVM
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Remember?")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
