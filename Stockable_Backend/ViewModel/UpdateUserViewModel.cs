using System.ComponentModel.DataAnnotations;

namespace Stockable_Backend.ViewModel
{
    public class UpdateUserViewModel
    {
        public string userFirstName { get; set; }
        public string userLastName { get; set; }
        public string userType { get; set; }
        public string phoneNumber { get; set; }
        public string? email { get; set; }
        public string? currentPassword { get; set; }
        public string? newPassword { get; set; }

        [Compare("newPassword", ErrorMessage = "Passwords do not match")]
        public string? confirmPassword { get; set; }
        public string? role { get; set; }
        public string? profilePicture { get; set; }

    }
}
