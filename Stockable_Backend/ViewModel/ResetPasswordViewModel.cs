using System.ComponentModel.DataAnnotations;

namespace Stockable_Backend.ViewModel
{
    public class ResetPasswordViewModel
    {
        public string userId { get; set; }
        public string token { get; set; }
        public string newPassword { get; set; }

        [Compare("newPassword", ErrorMessage = "Passwords do not match")]
        public string? confirmPassword { get; set; }
    }
}
