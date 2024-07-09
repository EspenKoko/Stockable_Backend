using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Stockable_Backend.Model
{
    public class User : IdentityUser
    {
        public string userFirstName { get; set; }
        public string userLastName { get; set; }
        public string userType { get; set; }
        public string? profilePicture { get; set; }
    }
}
